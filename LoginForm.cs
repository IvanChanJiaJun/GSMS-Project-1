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
    public partial class LoginForm : Form
    {
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        public LoginForm()
        {
            InitializeComponent();
        }

        // Handle the login button click event
        


        // Validate the customer credentials by querying the Customers table
        private bool ValidateCustomer(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(1) FROM Customers WHERE username = @username AND password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();

                return count == 1; // Return true if the customer exists
            }
        }

        // Validate the employee credentials by querying the Employees table
        private bool ValidateEmployee(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT role FROM Employees WHERE username = @username AND password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                var role = command.ExecuteScalar();
                connection.Close();

                return role != null; // Return true if the employee exists (regardless of role)
            }
        }

        // Get the role of the employee (either Admin or Employee)
        private string GetEmployeeRole(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT role FROM Employees WHERE username = @username AND password = @password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                connection.Open();
                var role = command.ExecuteScalar();
                connection.Close();

                if (role != null)
                {
                    return role.ToString();
                }

                return string.Empty; // Return empty string if no role is found
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        public class CustomerInfo
        {
            public int CustomerId { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }  // Add email
        }

        private CustomerInfo GetCustomerInfoFromLogin(string email)
        {
            CustomerInfo customerInfo = null;

            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GSMS.mdf;Integrated Security=True"))
            {
                string query = "SELECT customer_id, name, phone_number, email FROM Customers WHERE email = @Email";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    customerInfo = new CustomerInfo
                    {
                        CustomerId = Convert.ToInt32(reader["customer_id"]),
                        Name = reader["name"].ToString(),
                        PhoneNumber = reader["phone_number"].ToString(),
                        Email = reader["email"].ToString()  // Retrieve email
                    };
                }

                connection.Close();
            }

            return customerInfo;
        }

        private bool IsValidLogin(string email, string password)
        {
            using (SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\GSMS.mdf;Integrated Security=True"))
            {
                string query = "SELECT COUNT(*) FROM Customers WHERE email = @Email AND password = @Password";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);  // Make sure your database stores hashed/encrypted passwords for security

                connection.Open();
                int userCount = (int)command.ExecuteScalar();
                connection.Close();

                return userCount > 0;  // Return true if a user with the given email and password is found
            }
        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // First, check if the user is a customer
                string customerQuery = "SELECT customer_id, name, email, phone_number FROM Customers WHERE username = @username AND password = @password";
                using (SqlCommand cmd = new SqlCommand(customerQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Successful customer login
                            int customerId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string email = reader.GetString(2);
                            string phoneNumber = reader.GetString(3);

                            // Redirect to CustomerForm
                            CustomerForm customerForm = new CustomerForm(customerId, name, phoneNumber, email);
                            customerForm.Show();
                            this.Hide();
                            return;
                        }
                    }
                }

                // If not a customer, check if the user is an employee or admin
                string employeeQuery = "SELECT employee_id, name, role FROM Employees WHERE username = @username AND password = @password";
                using (SqlCommand cmd = new SqlCommand(employeeQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Successful employee or admin login
                            int employeeId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string role = reader.GetString(2);

                            if (role == "Admin")
                            {
                                // Redirect to AdminForm
                                AdminForm adminForm = new AdminForm(employeeId, name);
                                adminForm.Show();
                            }
                            else
                            {
                                // Redirect to EmployeeForm
                                EmployeeForm employeeForm = new EmployeeForm(employeeId, name);
                                employeeForm.Show();
                            }
                            this.Hide();
                        }
                        else
                        {
                            // Invalid login
                            MessageBox.Show("Invalid username or password.");
                        }
                    }
                }
            }
        }
    }
}
