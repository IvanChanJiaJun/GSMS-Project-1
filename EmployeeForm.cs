using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSMS_Project_1
{
    public partial class EmployeeForm : Form
    {
        private int employeeId;
        private string name;
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        public EmployeeForm()
        {
            InitializeComponent();
        }

        public EmployeeForm(int employeeId, string name)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            this.name = name;
            labelEmployeeName.Text = $"Welcome, {name}";
            LoadOrders();
            LoadProducts(); // Load products when form is initialized
        }

        // Load orders from the database and display them in the ListView
        private void LoadOrders()
        {
            lvOrders.Items.Clear();  // Clear any existing items in the ListView

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Update the query to also retrieve employee_id
                string query = "SELECT order_id, customer_id, order_date, status, employee_id FROM Orders";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a new ListViewItem for each order
                    ListViewItem item = new ListViewItem(reader["order_id"].ToString());
                    item.SubItems.Add(reader["customer_id"].ToString());
                    item.SubItems.Add(reader["order_date"].ToString());
                    item.SubItems.Add(reader["status"].ToString());
                    item.SubItems.Add(reader["employee_id"].ToString());  // Add employee_id to the ListView

                    lvOrders.Items.Add(item);  // Add the item to the ListView
                }

                connection.Close();
            }
        }

        // Load products from the database and display them in the ListView
        private void LoadProducts()
        {
            lvProducts.Items.Clear();  // Clear the current items from the ListView

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT product_id, name, price, stock_level FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["product_id"].ToString());
                    item.SubItems.Add(reader["name"].ToString());
                    item.SubItems.Add(reader["price"].ToString());
                    item.SubItems.Add(reader["stock_level"].ToString());

                    // Add the item to the ListView
                    lvProducts.Items.Add(item);
                }

                connection.Close();
            }
        }

        // Load order details based on the selected order
        private void lvOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count > 0)
            {
                string orderId = lvOrders.SelectedItems[0].SubItems[0].Text;
                LoadOrderDetails(orderId);
                UpdateTotalPrice(orderId); // Update the total price when an order is selected
            }
        }

        private void LoadOrderDetails(string orderId)
        {
            lvOrderDetails.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT od.product_id, p.name AS product_name, od.quantity, od.price FROM OrderDetails od " +
                               "INNER JOIN Products p ON od.product_id = p.product_id WHERE od.order_id = @orderId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["product_id"].ToString());
                    item.SubItems.Add(reader["product_name"].ToString());
                    item.SubItems.Add(reader["quantity"].ToString());
                    item.SubItems.Add(reader["price"].ToString());
                    lvOrderDetails.Items.Add(item);
                }

                connection.Close();
            }
        }

        // Add a new item to the order
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count > 0 && lvProducts.SelectedItems.Count > 0)
            {
                int orderId = int.Parse(lvOrders.SelectedItems[0].SubItems[0].Text);
                int productId = int.Parse(lvProducts.SelectedItems[0].SubItems[0].Text);

                if (!decimal.TryParse(lvProducts.SelectedItems[0].SubItems[2].Text, out decimal price))
                {
                    MessageBox.Show("Invalid price value.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        string insertOrderDetailsQuery = "INSERT INTO OrderDetails (order_id, product_id, quantity, price) VALUES (@orderId, @productId, @quantity, @price)";
                        SqlCommand command = new SqlCommand(insertOrderDetailsQuery, connection, transaction);
                        command.Parameters.AddWithValue("@orderId", orderId);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@price", price);
                        command.ExecuteNonQuery();

                        string insertTransactionQuery = "INSERT INTO InventoryTransactions (product_id, transaction_type, quantity, transaction_date) " +
                                                        "VALUES (@productId, 'Purchase', @quantity, @transactionDate)";
                        SqlCommand transactionCommand = new SqlCommand(insertTransactionQuery, connection, transaction);
                        transactionCommand.Parameters.AddWithValue("@productId", productId);
                        transactionCommand.Parameters.AddWithValue("@quantity", quantity);
                        transactionCommand.Parameters.AddWithValue("@transactionDate", DateTime.Now);
                        transactionCommand.ExecuteNonQuery();

                        transaction.Commit();
                        MessageBox.Show("Item added to the order successfully.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                txtQuantity.Clear();
                LoadOrderDetails(orderId.ToString());
                UpdateTotalPrice(orderId.ToString()); // Update total price
            }
            else
            {
                MessageBox.Show("Please select an order and a product to add.");
            }
        }

        // Edit the selected item in the order
        private void btnEditItem_Click_1(object sender, EventArgs e)
        {
            if (lvOrderDetails.SelectedItems.Count > 0)
            {
                string orderId = lvOrders.SelectedItems[0].SubItems[0].Text;
                string productId = lvOrderDetails.SelectedItems[0].SubItems[0].Text;
                string input = Prompt.ShowDialog("Enter new Quantity:", "Edit Quantity");

                if (!int.TryParse(input, out int newQuantity) || newQuantity <= 0)
                {
                    MessageBox.Show("Please enter a valid quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "UPDATE OrderDetails SET quantity = @quantity WHERE order_id = @orderId AND product_id = @productId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@quantity", newQuantity);
                    command.Parameters.AddWithValue("@orderId", orderId);
                    command.Parameters.AddWithValue("@productId", productId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadOrderDetails(orderId);
                UpdateTotalPrice(orderId); // Update total price
            }
            else
            {
                MessageBox.Show("Please select an item to edit.");
            }
        }

        // Delete the selected item from the order
        private void btnDeleteItem_Click_1(object sender, EventArgs e)
        {
            if (lvOrderDetails.SelectedItems.Count > 0)
            {
                string orderId = lvOrders.SelectedItems[0].SubItems[0].Text;
                string productId = lvOrderDetails.SelectedItems[0].SubItems[0].Text;
                int quantity = int.Parse(lvOrderDetails.SelectedItems[0].SubItems[2].Text);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        InsertTransaction(int.Parse(productId), "Refund", quantity, connection, transaction);

                        string updateStockQuery = "UPDATE Products SET stock_level = stock_level + @quantity WHERE product_id = @productId";
                        SqlCommand stockCommand = new SqlCommand(updateStockQuery, connection, transaction);
                        stockCommand.Parameters.AddWithValue("@quantity", quantity);
                        stockCommand.Parameters.AddWithValue("@productId", productId);
                        stockCommand.ExecuteNonQuery();

                        string query = "DELETE FROM OrderDetails WHERE order_id = @orderId AND product_id = @productId";
                        SqlCommand command = new SqlCommand(query, connection, transaction);
                        command.Parameters.AddWithValue("@orderId", orderId);
                        command.Parameters.AddWithValue("@productId", productId);
                        command.ExecuteNonQuery();

                        string checkOrderQuery = "SELECT COUNT(*) FROM OrderDetails WHERE order_id = @orderId";
                        SqlCommand checkOrderCommand = new SqlCommand(checkOrderQuery, connection, transaction);
                        checkOrderCommand.Parameters.AddWithValue("@orderId", orderId);
                        int remainingItems = (int)checkOrderCommand.ExecuteScalar();

                        if (remainingItems == 0)
                        {
                            string deleteOrderQuery = "DELETE FROM Orders WHERE order_id = @orderId";
                            SqlCommand deleteOrderCommand = new SqlCommand(deleteOrderQuery, connection, transaction);
                            deleteOrderCommand.Parameters.AddWithValue("@orderId", orderId);
                            deleteOrderCommand.ExecuteNonQuery();
                            lvOrders.SelectedItems[0].Remove();
                            MessageBox.Show("The order has been removed as it no longer contains any items.");
                        }

                        transaction.Commit();

                        if (remainingItems > 0)
                        {
                            LoadOrderDetails(orderId);
                            UpdateTotalPrice(orderId);
                        }
                        else
                        {
                            lvOrderDetails.Items.Clear();
                            lblTotalPrice.Text = "Total Price: $0.00";
                        }
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            if (transaction.Connection != null)
                            {
                                transaction.Rollback();
                            }
                        }
                        catch (InvalidOperationException rollbackEx)
                        {
                            MessageBox.Show("Transaction rollback failed: " + rollbackEx.Message);
                        }

                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

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

        private void btnDeleteOrder_Click_1(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count > 0)
            {
                string orderId = lvOrders.SelectedItems[0].SubItems[0].Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string deleteOrderDetailsQuery = "DELETE FROM OrderDetails WHERE order_id = @orderId";
                    SqlCommand orderDetailsCommand = new SqlCommand(deleteOrderDetailsQuery, connection);
                    orderDetailsCommand.Parameters.AddWithValue("@orderId", orderId);

                    string deleteOrderQuery = "DELETE FROM Orders WHERE order_id = @orderId";
                    SqlCommand orderCommand = new SqlCommand(deleteOrderQuery, connection);
                    orderCommand.Parameters.AddWithValue("@orderId", orderId);

                    connection.Open();
                    orderDetailsCommand.ExecuteNonQuery();
                    orderCommand.ExecuteNonQuery();
                    connection.Close();
                }

                LoadOrders();
                lvOrderDetails.Items.Clear();
                lblTotalPrice.Text = "Total Price: $0.00";
            }
            else
            {
                MessageBox.Show("Please select an order to delete.");
            }
        }

        private void UpdateTotalPrice(string orderId)
        {
            decimal totalPrice = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT SUM(price * quantity) AS total_price FROM OrderDetails WHERE order_id = @orderId";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@orderId", orderId);
                connection.Open();
                totalPrice = (decimal)command.ExecuteScalar();
                connection.Close();
            }

            lblTotalPrice.Text = $"Total Price: ${totalPrice:F2}";
        }

        private void lvOrderDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvOrderDetails.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvOrderDetails.SelectedItems[0];
                string productId = selectedItem.SubItems[0].Text;
                string productName = selectedItem.SubItems[1].Text;
                string quantity = selectedItem.SubItems[2].Text;
                MessageBox.Show($"Selected Product ID: {productId}\nProduct Name: {productName}\nQuantity: {quantity}");
            }
        }

        private void lvProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle product selection if needed
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            // Handle form load if necessary
        }

        private void btnLogout_Click_1(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cmbOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Handle combo box change if necessary
        }

        private void btnUpdateStatus_Click_1(object sender, EventArgs e)
        {
            if (lvOrders.SelectedItems.Count > 0)
            {
                string orderId = lvOrders.SelectedItems[0].SubItems[0].Text;  // Get the selected order's ID
                string newStatus = cmbOrderStatus.SelectedItem.ToString();     // Get the new status from the ComboBox
                int currentEmployeeId = this.employeeId;                       // Use the logged-in employee's ID

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Modify the query to update both status and employee_id
                    string query = "UPDATE Orders SET status = @status, employee_id = @employeeId WHERE order_id = @orderId";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@status", newStatus);
                    command.Parameters.AddWithValue("@employeeId", currentEmployeeId);  // Set the current employee ID
                    command.Parameters.AddWithValue("@orderId", orderId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                // Update the ListView to reflect the change in status and employee ID
                lvOrders.SelectedItems[0].SubItems[3].Text = newStatus;        // Update the status column
                lvOrders.SelectedItems[0].SubItems[4].Text = currentEmployeeId.ToString(); // Update the employee_id column

                MessageBox.Show("Order status and employee ID updated successfully.");
            }
            else
            {
                MessageBox.Show("Please select an order to update.");
            }
        }

        private void EmployeeForm_Load_1(object sender, EventArgs e)
        {

        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 50, Top = 20, Text = text };
            TextBox inputBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(confirmation);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? inputBox.Text : "";
        }
    }
}
