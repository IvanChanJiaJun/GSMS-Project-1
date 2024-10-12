using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace GSMS_Project_1
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm Form = new MainForm();
            Form.Show();
            this.Hide();

        }

        private void AdminForm_Load(object sender, EventArgs e)
        {

        }

        private int employeeId;
        private string name;

        public AdminForm(int employeeId, string name)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            this.name = name;

            // You can now use employeeId and name in your form
            labelEmployeeName.Text = $"Welcome, {name}";
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AccountForm Form = new AccountForm();
            Form.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnalysisForm Form = new AnalysisForm();
            Form.Show();
            this.Hide();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();  // Closes the entire application
        }
    }
}
