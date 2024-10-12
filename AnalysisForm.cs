using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSMS_Project_1
{
    public partial class AnalysisForm : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        public AnalysisForm()
        {
            InitializeComponent();
        }

        // This method is called when the form loads
        private void AnalysisForm_Load(object sender, EventArgs e)
        {
            // Optionally, you can load transactions automatically when the form loads
            LoadTransactions();
        }

        // Method to load transactions into the ListView
        private void LoadTransactions()
        {
            lvTransactions.Items.Clear(); // Clear any existing items

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT transaction_id, product_id, transaction_type, quantity, transaction_date FROM InventoryTransactions";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["transaction_id"].ToString());
                    item.SubItems.Add(reader["product_id"].ToString());
                    item.SubItems.Add(reader["transaction_type"].ToString());
                    item.SubItems.Add(reader["quantity"].ToString());
                    item.SubItems.Add(Convert.ToDateTime(reader["transaction_date"]).ToString("yyyy-MM-dd"));
                    lvTransactions.Items.Add(item);
                }

                connection.Close();
            }
        }

        // This event handler is for the Load Transactions button
        private void btnLoadTransactions_Click(object sender, EventArgs e)
        {
            LoadTransactions();
        }

        private void lvTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Get the path to the user's desktop
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string filePath = Path.Combine(desktopPath, "InventoryTransactions.csv");

            string query = "SELECT * FROM InventoryTransactions";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Create a StreamWriter to write to the file on the desktop
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the header line
                    writer.WriteLine("Transaction ID,Product ID,Transaction Type,Quantity,Transaction Date");

                    // Write each record
                    while (reader.Read())
                    {
                        string line = $"{reader["transaction_id"]},{reader["product_id"]},{reader["transaction_type"]},{reader["quantity"]},{reader["transaction_date"]}";
                        writer.WriteLine(line);
                    }
                }

                MessageBox.Show($"Transactions exported to {filePath}");

                // Clear the InventoryTransactions table after exporting
                ClearInventoryTransactions();

                // Reset the Transaction ID to start from 1
                ResetTransactionID();

                // Update the ListView to reflect the cleared data
                UpdateListView();
            }
        }

        // Method to clear the InventoryTransactions table
        private void ClearInventoryTransactions()
        {
            string deleteQuery = "DELETE FROM InventoryTransactions";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(deleteQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            //MessageBox.Show("InventoryTransactions table cleared.");
        }

        // Method to reset the Transaction ID to start from 1
        private void ResetTransactionID()
        {
            string resetQuery = "DBCC CHECKIDENT ('InventoryTransactions', RESEED, 0)";  // Resets the IDENTITY value to 0, so the next ID will be 1

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(resetQuery, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            //MessageBox.Show("Transaction ID reset to start from 1.");
        }

        // Method to update the ListView after clearing the data
        private void UpdateListView()
        {
            lvTransactions.Items.Clear(); // Clear the ListView items
            //MessageBox.Show("InventoryTransactions table cleared.");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            // Show the AdminForm
            adminForm.Show();
            this.Hide();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  // Closes the entire application
        }
    }
}
