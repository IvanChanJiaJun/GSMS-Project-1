using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace GSMS_Project_1
{
    public partial class CustomerForm : Form
    {
        private int customerId;
        private string customerName;
        private string customerPhoneNumber;
        private string customerEmail;

        public CustomerForm(int customerId, string customerName, string customerPhoneNumber, string customerEmail)
        {
            InitializeComponent();
            this.customerId = customerId;
            this.customerName = customerName;
            this.customerPhoneNumber = customerPhoneNumber;
            this.customerEmail = customerEmail;

            lblCustomerName.Text = customerName;
            lblCustomerPhone.Text = customerPhoneNumber;
            lblCustomerEmail.Text = customerEmail;

            LoadProducts();
        }

        // Connection string to the database
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        // Shopping cart list to hold CartItem objects
        private List<CartItem> shoppingCart = new List<CartItem>();

        // Load the products into the ListView (lvProducts)
        private void LoadProducts()
        {
            lvProducts.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT product_id, name, price FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["product_id"].ToString());
                    item.SubItems.Add(reader["name"].ToString());
                    item.SubItems.Add(reader["price"].ToString());
                    lvProducts.Items.Add(item);
                }

                connection.Close();
            }
        }

        // Add selected product to the shopping cart
        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            if (lvProducts.SelectedItems.Count > 0)
            {
                var selectedItem = lvProducts.SelectedItems[0];
                string productName = selectedItem.SubItems[1].Text;
                decimal productPrice = Convert.ToDecimal(selectedItem.SubItems[2].Text);

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (quantity > 10)
                {
                    MessageBox.Show("You cannot add more than 10 items of a single product.", "Quantity Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var cartItem = shoppingCart.FirstOrDefault(item => item.ProductName == productName);
                if (cartItem != null)
                {
                    if (cartItem.Quantity + quantity > 10)
                    {
                        MessageBox.Show("You cannot add more than 10 items of a single product.", "Quantity Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    cartItem.Quantity += quantity;
                }
                else
                {
                    shoppingCart.Add(new CartItem(productName, productPrice, quantity));
                }

                UpdateCartView();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select a product and enter a quantity.");
            }
        }

        // Update the cart view (lvCart) with current shopping cart items
        private void UpdateCartView()
        {
            lvCart.Items.Clear();
            decimal totalAmount = 0;

            foreach (var cartItem in shoppingCart)
            {
                ListViewItem item = new ListViewItem(cartItem.ProductName);
                item.SubItems.Add(cartItem.Quantity.ToString());
                lvCart.Items.Add(item);

                totalAmount += cartItem.ProductPrice * cartItem.Quantity;
            }

            lblTotalAmountValue.Text = totalAmount.ToString("F2");
        }

        // Process checkout and store data into Orders and OrderDetails
        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                MessageBox.Show("Your cart is empty. Please add items before proceeding to checkout.", "Empty Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // Insert a new order into Orders table and retrieve order ID
                    string insertOrderQuery = "INSERT INTO Orders (customer_id, order_date, status) OUTPUT INSERTED.order_id VALUES (@customerId, @orderDate, @status)";
                    SqlCommand orderCommand = new SqlCommand(insertOrderQuery, connection, transaction);
                    orderCommand.Parameters.AddWithValue("@customerId", customerId);
                    orderCommand.Parameters.AddWithValue("@orderDate", DateTime.Now);
                    orderCommand.Parameters.AddWithValue("@status", "Pending");
                    int orderId = (int)orderCommand.ExecuteScalar();

                    // Insert each product in the shopping cart into OrderDetails table
                    foreach (var cartItem in shoppingCart)
                    {
                        decimal price = GetProductPriceByName(cartItem.ProductName, connection, transaction);
                        int productId = GetProductIdByName(cartItem.ProductName, connection, transaction);

                        // Insert into OrderDetails
                        string insertOrderDetailsQuery = "INSERT INTO OrderDetails (order_id, product_id, quantity, price) VALUES (@orderId, @productId, @quantity, @price)";
                        SqlCommand orderDetailsCommand = new SqlCommand(insertOrderDetailsQuery, connection, transaction);
                        orderDetailsCommand.Parameters.AddWithValue("@orderId", orderId);
                        orderDetailsCommand.Parameters.AddWithValue("@productId", productId);
                        orderDetailsCommand.Parameters.AddWithValue("@quantity", cartItem.Quantity);
                        orderDetailsCommand.Parameters.AddWithValue("@price", price);
                        orderDetailsCommand.ExecuteNonQuery();

                        // Insert into InventoryTransactions (Purchase)
                        InsertTransaction(productId, "Purchase", cartItem.Quantity, connection, transaction);

                        // Update stock level for the purchased product
                        string updateStockQuery = "UPDATE Products SET stock_level = stock_level - @quantity WHERE product_id = @productId";
                        SqlCommand stockCommand = new SqlCommand(updateStockQuery, connection, transaction);
                        stockCommand.Parameters.AddWithValue("@quantity", cartItem.Quantity);
                        stockCommand.Parameters.AddWithValue("@productId", productId);
                        stockCommand.ExecuteNonQuery();
                    }

                    // Commit transaction
                    transaction.Commit();
                    MessageBox.Show("Order placed successfully!");

                    // Clear the cart after successful transaction
                    shoppingCart.Clear();
                    UpdateCartView();
                }
                catch (Exception ex)
                {
                    // Rollback transaction if an error occurs
                    transaction.Rollback();
                    MessageBox.Show("An error occurred during checkout: " + ex.Message);
                }

                connection.Close();
            }
        }


        // Insert transaction into InventoryTransactions
        private void InsertTransaction(int productId, string transactionType, int quantity, SqlConnection connection, SqlTransaction transaction = null)
        {
            string insertTransactionQuery = "INSERT INTO InventoryTransactions (product_id, transaction_type, quantity, transaction_date) VALUES (@productId, @transactionType, @quantity, @transactionDate)";
            SqlCommand transactionCommand = new SqlCommand(insertTransactionQuery, connection, transaction);
            transactionCommand.Parameters.AddWithValue("@productId", productId);
            transactionCommand.Parameters.AddWithValue("@transactionType", transactionType);
            transactionCommand.Parameters.AddWithValue("@quantity", quantity);
            transactionCommand.Parameters.AddWithValue("@transactionDate", DateTime.Now);
            transactionCommand.ExecuteNonQuery();
        }

        // Get product price by name
        private decimal GetProductPriceByName(string productName, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT price FROM Products WHERE name = @productName";
            SqlCommand command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@productName", productName);
            return (decimal)command.ExecuteScalar();
        }

        // Get product ID by name
        private int GetProductIdByName(string productName, SqlConnection connection, SqlTransaction transaction)
        {
            string query = "SELECT product_id FROM Products WHERE name = @productName";
            SqlCommand command = new SqlCommand(query, connection, transaction);
            command.Parameters.AddWithValue("@productName", productName);
            return (int)command.ExecuteScalar();
        }

        private void ClearForm()
        {
            txtQuantity.Clear();
        }

        private void btnEditCartItem_Click(object sender, EventArgs e)
        {
            if (lvCart.SelectedItems.Count > 0)
            {
                string productName = lvCart.SelectedItems[0].Text;

                if (!int.TryParse(txtQuantity.Text, out int newQuantity) || newQuantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (newQuantity > 10)
                {
                    MessageBox.Show("You cannot add more than 10 items of a single product.", "Quantity Limit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var itemToEdit = shoppingCart.FirstOrDefault(i => i.ProductName == productName);
                if (itemToEdit != null)
                {
                    itemToEdit.Quantity = newQuantity;
                }

                UpdateCartView();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select an item to edit.");
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (lvCart.SelectedItems.Count > 0)
            {
                string productName = lvCart.SelectedItems[0].Text;
                var itemToRemove = shoppingCart.FirstOrDefault(i => i.ProductName == productName);

                if (itemToRemove != null)
                {
                    shoppingCart.Remove(itemToRemove);
                }

                UpdateCartView();
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Implement logout logic here
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }

    // CartItem class to represent each item in the cart
    public class CartItem
    {
        public string ProductName { get; }
        public decimal ProductPrice { get; }
        public int Quantity { get; set; }

        public CartItem(string productName, decimal productPrice, int quantity)
        {
            ProductName = productName;
            ProductPrice = productPrice;
            Quantity = quantity;
        }
    }

}
