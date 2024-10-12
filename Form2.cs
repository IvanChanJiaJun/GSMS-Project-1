using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace GSMS_Project_1
{
    public partial class MainForm : Form
    {
        // Connection string to connect to your SQL Server database
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        public MainForm()
        {
            InitializeComponent();
            LoadProducts();
        }

        // Load products from the database and display them in the ListView
        private void LoadProducts()
        {
            listViewProducts.Items.Clear(); // Clear the ListView before loading data

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT product_id, name, price, stock_level, supplier_id FROM Products";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    // Create a new ListViewItem for each product
                    ListViewItem item = new ListViewItem(reader["product_id"].ToString()); // First column: Product ID
                    item.SubItems.Add(reader["name"].ToString());                         // Second column: Name
                    item.SubItems.Add(reader["price"].ToString());                        // Third column: Price

                    // Convert integers to strings
                    item.SubItems.Add(reader["stock_level"].ToString());                  // Fourth column: Stock Level (Integer)
                    item.SubItems.Add(reader["supplier_id"].ToString());                  // Fifth column: Supplier ID (Integer)

                    // Add the ListViewItem to the ListView
                    listViewProducts.Items.Add(item);
                }

                connection.Close();
            }
        }



        // Load suppliers from the database and display them in the ListView
        private void LoadSuppliers()
        {
            listViewSuppliers.Items.Clear(); // Assuming you have a ListView for suppliers

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT supplier_id, name, contact_info FROM Suppliers";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["supplier_id"].ToString());
                    item.SubItems.Add(reader["name"].ToString());
                    item.SubItems.Add(reader["contact_info"].ToString());
                    listViewSuppliers.Items.Add(item);
                }

                connection.Close();
            }
        }


        // Add a new product to the database
        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            // Ensure the product name, price, and stock are provided and valid
            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                MessageBox.Show("Product name cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Please enter a valid price greater than zero.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtStock.Text, out int stock) || stock < 0)
            {
                MessageBox.Show("Please enter a valid stock quantity (cannot be negative).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtSupplierID.Text, out int supplierID))
            {
                MessageBox.Show("Please enter a valid supplier ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the product name already exists in the database
                string checkProductQuery = "SELECT COUNT(*) FROM Products WHERE name = @productName";
                SqlCommand checkProductCommand = new SqlCommand(checkProductQuery, connection);
                checkProductCommand.Parameters.AddWithValue("@productName", txtProductName.Text);

                int productExists = (int)checkProductCommand.ExecuteScalar();
                if (productExists > 0)
                {
                    MessageBox.Show("A product with the same name already exists. Please use a different name.", "Duplicate Product", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    return;
                }

                // Check if the supplier exists in the Suppliers table
                string checkSupplierQuery = "SELECT COUNT(*) FROM Suppliers WHERE supplier_id = @supplierID";
                SqlCommand checkSupplierCommand = new SqlCommand(checkSupplierQuery, connection);
                checkSupplierCommand.Parameters.AddWithValue("@supplierID", supplierID);

                int supplierExists = (int)checkSupplierCommand.ExecuteScalar();
                if (supplierExists == 0)
                {
                    MessageBox.Show("Supplier does not exist. Please enter a valid supplier ID.", "Invalid Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    return;
                }

                // Insert the product if the supplier exists and no duplicate product is found
                string query = "INSERT INTO Products (name, price, stock_level, supplier_id) VALUES (@name, @price, @stock, @supplierID)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", txtProductName.Text);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@stock", stock);
                command.Parameters.AddWithValue("@supplierID", supplierID);

                command.ExecuteNonQuery();
                connection.Close();
            }

            // Reload products and clear the form
            LoadProducts();
            ClearForm();
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

        // Update an existing product in the database
        private void btnEditProduct_Click_1(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count > 0)
            {
                // Retrieve the selected Product ID from the ListView
                string productId = listViewProducts.SelectedItems[0].SubItems[0].Text;

                // Ensure the product name, price, and stock are provided and valid
                if (string.IsNullOrWhiteSpace(txtProductName.Text))
                {
                    MessageBox.Show("Product name cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtPrice.Text, out decimal price) || price <= 0)
                {
                    MessageBox.Show("Please enter a valid price greater than zero.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtStock.Text, out int newStock) || newStock < 0)
                {
                    MessageBox.Show("Please enter a valid stock quantity (cannot be negative).", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtSupplierID.Text, out int supplierID))
                {
                    MessageBox.Show("Please enter a valid supplier ID.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Check if the supplier exists in the Suppliers table
                        string checkSupplierQuery = "SELECT COUNT(*) FROM Suppliers WHERE supplier_id = @supplierID";
                        SqlCommand checkSupplierCommand = new SqlCommand(checkSupplierQuery, connection, transaction);
                        checkSupplierCommand.Parameters.AddWithValue("@supplierID", supplierID);

                        int supplierExists = (int)checkSupplierCommand.ExecuteScalar();
                        if (supplierExists == 0)
                        {
                            MessageBox.Show("Supplier does not exist. Please enter a valid supplier ID.", "Invalid Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }

                        // Retrieve the current stock level before updating
                        string getCurrentStockQuery = "SELECT stock_level FROM Products WHERE product_id = @productId";
                        SqlCommand getStockCommand = new SqlCommand(getCurrentStockQuery, connection, transaction);
                        getStockCommand.Parameters.AddWithValue("@productId", productId);
                        int currentStock = (int)getStockCommand.ExecuteScalar();

                        // Update the product in the Products table
                        string updateQuery = "UPDATE Products SET name = @name, price = @price, stock_level = @stock, supplier_id = @supplierID WHERE product_id = @id";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection, transaction);
                        updateCommand.Parameters.AddWithValue("@id", productId);
                        updateCommand.Parameters.AddWithValue("@name", txtProductName.Text);
                        updateCommand.Parameters.AddWithValue("@price", price);
                        updateCommand.Parameters.AddWithValue("@stock", newStock);
                        updateCommand.Parameters.AddWithValue("@supplierID", supplierID);
                        updateCommand.ExecuteNonQuery();

                        // Log the restock if the stock level has increased
                        if (newStock > currentStock)
                        {
                            int restockedQuantity = newStock - currentStock;
                            InsertTransaction(int.Parse(productId), "Restock", restockedQuantity, connection, transaction);
                        }

                        // Commit the transaction
                        transaction.Commit();
                        MessageBox.Show("Product updated successfully.");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Error updating product: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                // Reload the updated product list and clear the form
                LoadProducts();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select a product to edit.", "No Product Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Delete a product from the database
        private void btnDeleteProduct_Click_1(object sender, EventArgs e)
        {
            // Check if an item is selected in the ListView
            if (listViewProducts.SelectedItems.Count > 0)
            {
                string productId = listViewProducts.SelectedItems[0].SubItems[0].Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Products WHERE product_id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", productId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadProducts();
                ClearForm();
            }
            else
            {
                // Display a message to the user if nothing is selected
                MessageBox.Show("Please select a product to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        // Clear the form fields
        private void ClearForm()
        {
            txtProductID.Clear();
            txtProductName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            txtSupplierID.Clear();
        }

        // Handle ListView selection change to populate the form with selected product details
        private void listViewProducts_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (listViewProducts.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewProducts.SelectedItems[0];

                // Update TextBoxes with the selected product's data
                txtProductName.Text = selectedItem.SubItems[1].Text;  // Product Name
                txtPrice.Text = selectedItem.SubItems[2].Text;        // Price
                txtStock.Text = selectedItem.SubItems[3].Text;        // Stock Level
                txtSupplierID.Text = selectedItem.SubItems[4].Text;   // Supplier ID

                // Optional: If you want to display the Product ID (read-only if needed)
                // You can create a read-only field for Product ID
                txtProductID.Text = selectedItem.SubItems[0].Text;    // Product ID
            }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
            LoadSuppliers();
        }



        private void listViewSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (listViewSuppliers.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewSuppliers.SelectedItems[0];
                txtSupplierName.Text = selectedItem.SubItems[1].Text;
                txtSupplierContactInfo.Text = selectedItem.SubItems[2].Text;
            }

        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            // Validate that the supplier name is not empty
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text))
            {
                MessageBox.Show("Supplier name cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validate that the contact info is not empty
            if (string.IsNullOrWhiteSpace(txtSupplierContactInfo.Text))
            {
                MessageBox.Show("Contact information cannot be empty.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string supplierName = txtSupplierName.Text;
            string contactInfo = txtSupplierContactInfo.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Check if the supplier name already exists in the database
                string checkSupplierQuery = "SELECT COUNT(*) FROM Suppliers WHERE name = @name";
                SqlCommand checkSupplierCommand = new SqlCommand(checkSupplierQuery, connection);
                checkSupplierCommand.Parameters.AddWithValue("@name", supplierName);

                int supplierExists = (int)checkSupplierCommand.ExecuteScalar();
                if (supplierExists > 0)
                {
                    MessageBox.Show("A supplier with the same name already exists. Please use a different name.", "Duplicate Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    connection.Close();
                    return;
                }

                // Insert the new supplier into the Suppliers table
                string query = "INSERT INTO Suppliers (name, contact_info) VALUES (@name, @contactInfo)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@name", supplierName);
                command.Parameters.AddWithValue("@contactInfo", contactInfo);

                command.ExecuteNonQuery();
                connection.Close();
            }

            LoadSuppliers();
            ClearSupplierForm();
        }


        private void btnEditSupplier_Click(object sender, EventArgs e)
        {
            if (listViewSuppliers.SelectedItems.Count > 0)
            {
                string supplierId = listViewSuppliers.SelectedItems[0].SubItems[0].Text;
                string supplierName = txtSupplierName.Text;
                string contactInfo = txtSupplierContactInfo.Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Check if the updated supplier name already exists in the database (excluding the current supplier being edited)
                    string checkSupplierQuery = "SELECT COUNT(*) FROM Suppliers WHERE name = @name AND supplier_id != @id";
                    SqlCommand checkSupplierCommand = new SqlCommand(checkSupplierQuery, connection);
                    checkSupplierCommand.Parameters.AddWithValue("@name", supplierName);
                    checkSupplierCommand.Parameters.AddWithValue("@id", supplierId);

                    int supplierExists = (int)checkSupplierCommand.ExecuteScalar();
                    if (supplierExists > 0)
                    {
                        MessageBox.Show("A supplier with the same name already exists. Please use a different name.", "Duplicate Supplier", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        connection.Close();
                        return;
                    }

                    // Update the supplier's information in the Suppliers table
                    string query = "UPDATE Suppliers SET name = @name, contact_info = @contactInfo WHERE supplier_id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", supplierId);
                    command.Parameters.AddWithValue("@name", supplierName);
                    command.Parameters.AddWithValue("@contactInfo", contactInfo);

                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadSuppliers();
                ClearSupplierForm();
            }
        }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            if (listViewSuppliers.SelectedItems.Count > 0)
            {
                string supplierId = listViewSuppliers.SelectedItems[0].SubItems[0].Text;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Suppliers WHERE supplier_id = @id";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@id", supplierId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }

                LoadSuppliers();
                ClearSupplierForm();
            }
        }
        private void ClearSupplierForm()
        {
            txtSupplierName.Clear();
            txtSupplierContactInfo.Clear();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            // Create an instance of LoginForm
            LoginForm loginForm = new LoginForm();

            // Show the LoginForm
            loginForm.Show();

            // Optionally, hide or close the current form (MainForm)
            this.Hide();  // Hides the current MainForm
                          // this.Close();  // Alternatively, closes the current form
        }

        private void btnback_Click_1(object sender, EventArgs e)
        {
            // Create an instance of AdminForm
            AdminForm adminForm = new AdminForm();
            // Show the AdminForm
            adminForm.Show();
            this.Hide();
        }

        private void btnexit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();  // Closes the entire application
        }

    }
}

