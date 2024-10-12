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
    public partial class AccountForm : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        public AccountForm()
        {
            InitializeComponent();
            LoadEmployees();
            LoadCustomers();

            // Attach event handlers for selection changes
            lvEmployees.ItemSelectionChanged += LvEmployees_ItemSelectionChanged;
            lvCustomers.ItemSelectionChanged += LvCustomers_ItemSelectionChanged;
        }

        // Ensure only one item is selected at a time
        private void LvEmployees_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvEmployees.SelectedItems.Count > 0)
            {
                // Clear the selection in the Customers list when an Employee is selected
                lvCustomers.SelectedItems.Clear();
            }
        }

        private void LvCustomers_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (lvCustomers.SelectedItems.Count > 0)
            {
                // Clear the selection in the Employees list when a Customer is selected
                lvEmployees.SelectedItems.Clear();
            }
        }

        // Load employees including username and password
        private void LoadEmployees()
        {
            lvEmployees.Items.Clear(); // Clear any existing items

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT employee_id, name, position, role, shift_schedule, employee_phone_number, username, password FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["employee_id"].ToString());
                    item.SubItems.Add(reader["name"].ToString());
                    item.SubItems.Add(reader["position"].ToString());
                    item.SubItems.Add(reader["role"].ToString());
                    item.SubItems.Add(reader["shift_schedule"].ToString()); // Populate shift
                    item.SubItems.Add(reader["employee_phone_number"].ToString()); // Populate phone number
                    item.SubItems.Add(reader["username"].ToString());
                    item.SubItems.Add(reader["password"].ToString());

                    lvEmployees.Items.Add(item);
                }

                connection.Close();
            }
        }

        // Load customers including username and password
        private void LoadCustomers()
        {
            lvCustomers.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT customer_id, name, email, phone_number, username, password FROM Customers";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["customer_id"].ToString());
                    item.SubItems.Add(reader["name"].ToString());
                    item.SubItems.Add(reader["email"].ToString());
                    item.SubItems.Add(reader["phone_number"].ToString());
                    item.SubItems.Add(reader["username"].ToString());
                    item.SubItems.Add(reader["password"].ToString());
                    lvCustomers.Items.Add(item);
                }

                connection.Close();
            }
        }

        private void btnAddUser_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to add an employee?", "Choose User Type", MessageBoxButtons.YesNo);
            bool isEmployee = (result == DialogResult.Yes);

            using (AddUserForm addUserForm = new AddUserForm(isEmployee))
            {
                if (addUserForm.ShowDialog() == DialogResult.OK)
                {
                    if (isEmployee)
                    {
                        LoadEmployees();
                    }
                    else
                    {
                        LoadCustomers();
                    }
                }
            }
        }

        // Edit user button
        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (lvEmployees.SelectedItems.Count > 0 || lvCustomers.SelectedItems.Count > 0)
            {
                bool isEmployee = lvEmployees.SelectedItems.Count > 0;
                ListViewItem selectedItem = isEmployee ? lvEmployees.SelectedItems[0] : lvCustomers.SelectedItems[0];
                int userId = int.Parse(selectedItem.SubItems[0].Text);

                EditUserForm editUserForm = new EditUserForm(userId, isEmployee);

                // Subscribe to the UserUpdated event
                editUserForm.UserUpdated += (s, ev) =>
                {
                    if (isEmployee)
                    {
                        LoadEmployees();  // Reload employees when updated
                    }
                    else
                    {
                        LoadCustomers();  // Reload customers when updated
                    }
                };

                editUserForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a user to edit.");
            }
        }

        // Delete user button
        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (lvEmployees.SelectedItems.Count > 0 || lvCustomers.SelectedItems.Count > 0)
            {
                bool isEmployee = lvEmployees.SelectedItems.Count > 0;
                ListViewItem selectedItem = isEmployee ? lvEmployees.SelectedItems[0] : lvCustomers.SelectedItems[0];
                int userId = int.Parse(selectedItem.SubItems[0].Text);

                DialogResult result = MessageBox.Show($"Are you sure you want to delete this {(isEmployee ? "employee" : "customer")}?", "Confirm Delete", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        string deleteQuery = isEmployee
                            ? "DELETE FROM Employees WHERE employee_id = @userId"
                            : "DELETE FROM Customers WHERE customer_id = @userId";

                        SqlCommand command = new SqlCommand(deleteQuery, connection);
                        command.Parameters.AddWithValue("@userId", userId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }

                    MessageBox.Show($"{(isEmployee ? "Employee" : "Customer")} deleted successfully!");

                    if (isEmployee)
                    {
                        LoadEmployees();
                    }
                    else
                    {
                        LoadCustomers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            this.Hide();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  // Closes the entire application
        }
    }
}

