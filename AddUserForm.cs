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
    public partial class AddUserForm : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";
        private bool isEmployee;

        public AddUserForm(bool isEmployee)
        {
            InitializeComponent();
            this.isEmployee = isEmployee;

            // Update UI labels and placeholders based on whether we are adding an employee or a customer
            if (isEmployee)
            {
                lblTitle.Text = "Add Employee";
                lblEmailOrPosition.Text = "Position";
                lblPhoneOrRole.Text = "Role";
                lblShift.Visible = true;
                txtShift.Visible = true;
                lblEmployeePhone.Visible = true;
                txtEmployeePhone.Visible = true;
            }
            else
            {
                lblTitle.Text = "Add Customer";
                lblEmailOrPosition.Text = "Email";
                lblPhoneOrRole.Text = "Phone Number";
                lblShift.Visible = false;
                txtShift.Visible = false;
                lblEmployeePhone.Visible = false;
                txtEmployeePhone.Visible = false;
            }
        }

        // Method called when the "Save" button is clicked
        private void btnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string emailOrPosition = txtEmailOrPosition.Text;
            string phoneOrRole = txtPhoneOrRole.Text;
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Validate that necessary fields are not empty
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all required fields (Name, Username, Password).", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if username already exists in both Customers and Employees tables
            if (IsUsernameTaken(username))
            {
                MessageBox.Show("This username is already taken by either an employee or a customer. Please choose a different one.", "Username Taken", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Depending on whether we are adding an employee or customer, call the relevant method
                if (isEmployee)
                {
                    string shift = txtShift.Text;
                    string employeePhone = txtEmployeePhone.Text;

                    if (string.IsNullOrWhiteSpace(shift) || string.IsNullOrWhiteSpace(employeePhone))
                    {
                        MessageBox.Show("Please fill in all required fields (Shift, Phone Number for Employee).", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    AddEmployee(name, emailOrPosition, phoneOrRole, username, password, shift, employeePhone);
                }
                else
                {
                    AddCustomer(name, emailOrPosition, phoneOrRole, username, password);
                }

                MessageBox.Show($"{(isEmployee ? "Employee" : "Customer")} added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Method to check if a username is already taken in both Customers and Employees tables
        private bool IsUsernameTaken(string username)
        {
            bool isTaken = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkUsernameQuery = @"
                    SELECT COUNT(*) 
                    FROM Employees 
                    WHERE username = @username 
                    UNION ALL 
                    SELECT COUNT(*) 
                    FROM Customers 
                    WHERE username = @username";

                SqlCommand command = new SqlCommand(checkUsernameQuery, connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    if (Convert.ToInt32(reader[0]) > 0)
                    {
                        isTaken = true;
                        break;
                    }
                }

                connection.Close();
            }

            return isTaken;
        }

        // Method to generate the next employee ID manually
        private int GetNextEmployeeId()
        {
            int nextId = 1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(employee_id) FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    nextId = Convert.ToInt32(result) + 1;
                }
                connection.Close();
            }
            return nextId;
        }

        // Method to generate the next customer ID manually
        private int GetNextCustomerId()
        {
            int nextId = 1;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT MAX(customer_id) FROM Customers";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                var result = command.ExecuteScalar();
                if (result != DBNull.Value)
                {
                    nextId = Convert.ToInt32(result) + 1;
                }
                connection.Close();
            }
            return nextId;
        }

        // Method to add an employee to the Employees table with sequential IDs
        private void AddEmployee(string name, string position, string role, string username, string password, string shift, string employeePhone)
        {
            try
            {
                int employeeId = GetNextEmployeeId(); // Get the next available employee ID

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // Enable IDENTITY_INSERT to manually assign employee_id
                        string enableIdentityInsert = "SET IDENTITY_INSERT Employees ON";
                        SqlCommand enableCommand = new SqlCommand(enableIdentityInsert, connection, transaction);
                        enableCommand.ExecuteNonQuery();

                        // Insert employee into the database
                        string query = "INSERT INTO Employees (employee_id, name, position, shift_schedule, employee_phone_number, username, password, role) " +
                                       "VALUES (@employeeId, @name, @position, @shift, @phoneNumber, @username, @password, @role)";
                        SqlCommand command = new SqlCommand(query, connection, transaction);

                        // Add the necessary parameters to the SQL query
                        command.Parameters.AddWithValue("@employeeId", employeeId);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@position", position);
                        command.Parameters.AddWithValue("@shift", shift); // Use the value entered for shift
                        command.Parameters.AddWithValue("@phoneNumber", employeePhone); // Use the value entered for phone number
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password); // Ideally, password should be hashed
                        command.Parameters.AddWithValue("@role", role);

                        // Execute the SQL command
                        command.ExecuteNonQuery();

                        // Disable IDENTITY_INSERT after the insert
                        string disableIdentityInsert = "SET IDENTITY_INSERT Employees OFF";
                        SqlCommand disableCommand = new SqlCommand(disableIdentityInsert, connection, transaction);
                        disableCommand.ExecuteNonQuery();

                        // Commit the transaction to ensure the data is saved
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction in case of an error
                        transaction.Rollback();
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
        }

        // Method to add a customer to the Customers table with sequential IDs
        private void AddCustomer(string name, string email, string phoneNumber, string username, string password)
        {
            try
            {
                int customerId = GetNextCustomerId(); // Get the next available customer ID

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        // Enable IDENTITY_INSERT to manually assign customer_id
                        string enableIdentityInsert = "SET IDENTITY_INSERT Customers ON";
                        SqlCommand enableCommand = new SqlCommand(enableIdentityInsert, connection, transaction);
                        enableCommand.ExecuteNonQuery();

                        // Insert customer into the database
                        string query = "INSERT INTO Customers (customer_id, name, email, phone_number, username, password) " +
                                       "VALUES (@customerId, @name, @email, @phoneNumber, @username, @password)";
                        SqlCommand command = new SqlCommand(query, connection, transaction);

                        command.Parameters.AddWithValue("@customerId", customerId);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        command.ExecuteNonQuery();

                        // Disable IDENTITY_INSERT after the insert
                        string disableIdentityInsert = "SET IDENTITY_INSERT Customers OFF";
                        SqlCommand disableCommand = new SqlCommand(disableIdentityInsert, connection, transaction);
                        disableCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("An error occurred: " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error connecting to the database: " + ex.Message);
            }
        }
    }
}
