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
    public partial class EditUserForm : Form
    {
        private int userId;
        private bool isEmployee;
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\isaac\\Desktop\\GSMS Project 1\\GSMS.mdf\";Integrated Security=True";

        // Event to notify the AccountForm that the user has been updated
        public event EventHandler UserUpdated;

        public EditUserForm(int userId, bool isEmployee)
        {
            InitializeComponent();
            this.userId = userId;
            this.isEmployee = isEmployee;
            ConfigureFormForUserType();
            LoadUserDetails();
        }

        // Configure the form depending on whether it's an Employee or Customer
        private void ConfigureFormForUserType()
        {
            if (isEmployee)
            {
                lblPosition.Visible = true;
                txtPosition.Visible = true;
                lblRole.Visible = true;
                txtRole.Visible = true;
                lblShift.Visible = true;
                txtShift.Visible = true;
                lblPhoneNumber.Visible = true;
                txtPhoneNumber.Visible = true;

                lblEmail.Visible = false;
                txtEmail.Visible = false;
            }
            else
            {
                lblPosition.Visible = false;
                txtPosition.Visible = false;
                lblRole.Visible = false;
                txtRole.Visible = false;
                lblShift.Visible = false;
                txtShift.Visible = false;

                lblEmail.Visible = true;
                txtEmail.Visible = true;
                lblPhoneNumber.Visible = true;
                txtPhoneNumber.Visible = true;
            }
        }

        // Load user details for editing
        private void LoadUserDetails()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = isEmployee
                    ? "SELECT name, position, role, shift_schedule, employee_phone_number, username, password FROM Employees WHERE employee_id = @userId"
                    : "SELECT name, email, phone_number, username, password FROM Customers WHERE customer_id = @userId";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@userId", userId);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    txtName.Text = reader["name"].ToString();
                    txtUsername.Text = reader["username"].ToString();
                    txtPassword.Text = reader["password"].ToString();

                    if (isEmployee)
                    {
                        txtPosition.Text = reader["position"].ToString();
                        txtRole.Text = reader["role"].ToString();
                        txtShift.Text = reader["shift_schedule"].ToString();
                        txtPhoneNumber.Text = reader["employee_phone_number"].ToString();
                    }
                    else
                    {
                        txtEmail.Text = reader["email"].ToString();
                        txtPhoneNumber.Text = reader["phone_number"].ToString();
                    }
                }

                connection.Close();
            }
        }

        // Save edited user details
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string updateQuery = isEmployee
                    ? "UPDATE Employees SET name = @name, position = @position, role = @role, shift_schedule = @shift, employee_phone_number = @phoneNumber, username = @username, password = @password WHERE employee_id = @userId"
                    : "UPDATE Customers SET name = @name, email = @email, phone_number = @phoneNumber, username = @username, password = @password WHERE customer_id = @userId";

                SqlCommand command = new SqlCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@userId", userId);
                command.Parameters.AddWithValue("@name", txtName.Text);
                command.Parameters.AddWithValue("@username", txtUsername.Text);
                command.Parameters.AddWithValue("@password", txtPassword.Text);

                if (isEmployee)
                {
                    command.Parameters.AddWithValue("@position", txtPosition.Text);
                    command.Parameters.AddWithValue("@role", txtRole.Text);
                    command.Parameters.AddWithValue("@shift", txtShift.Text);
                    command.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text);
                }
                else
                {
                    command.Parameters.AddWithValue("@email", txtEmail.Text);
                    command.Parameters.AddWithValue("@phoneNumber", txtPhoneNumber.Text);
                }

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }

            // Trigger the UserUpdated event
            UserUpdated?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("User details updated successfully!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // Cancel editing
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
