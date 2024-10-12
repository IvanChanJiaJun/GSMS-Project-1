namespace GSMS_Project_1
{
    partial class AccountForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnAddUser = new System.Windows.Forms.Button();
            this.btnEditUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.lvEmployees = new System.Windows.Forms.ListView();
            this.employeeIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeeNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeePositionColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeeRoleColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeeShiftColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeePhoneNumberColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeeUsernameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeePasswordColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvCustomers = new System.Windows.Forms.ListView();
            this.customerIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customerNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customerEmailColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customerPhoneNumberColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customerUsernameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customerPasswordColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAddUser
            // 
            this.btnAddUser.Location = new System.Drawing.Point(856, 25);
            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Size = new System.Drawing.Size(100, 30);
            this.btnAddUser.TabIndex = 0;
            this.btnAddUser.Text = "Add User";
            this.btnAddUser.UseVisualStyleBackColor = true;
            this.btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click_1);
            // 
            // btnEditUser
            // 
            this.btnEditUser.Location = new System.Drawing.Point(856, 85);
            this.btnEditUser.Name = "btnEditUser";
            this.btnEditUser.Size = new System.Drawing.Size(100, 30);
            this.btnEditUser.TabIndex = 1;
            this.btnEditUser.Text = "Edit User";
            this.btnEditUser.UseVisualStyleBackColor = true;
            this.btnEditUser.Click += new System.EventHandler(this.btnEditUser_Click);
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(856, 147);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteUser.TabIndex = 2;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            this.btnDeleteUser.Click += new System.EventHandler(this.btnDeleteUser_Click);
            // 
            // lvEmployees
            // 
            this.lvEmployees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.employeeIdColumn,
            this.employeeNameColumn,
            this.employeePositionColumn,
            this.employeeRoleColumn,
            this.employeeShiftColumn,
            this.employeePhoneNumberColumn,
            this.employeeUsernameColumn,
            this.employeePasswordColumn});
            this.lvEmployees.FullRowSelect = true;
            this.lvEmployees.GridLines = true;
            this.lvEmployees.HideSelection = false;
            this.lvEmployees.Location = new System.Drawing.Point(12, 12);
            this.lvEmployees.Name = "lvEmployees";
            this.lvEmployees.Size = new System.Drawing.Size(800, 180);
            this.lvEmployees.TabIndex = 3;
            this.lvEmployees.UseCompatibleStateImageBehavior = false;
            this.lvEmployees.View = System.Windows.Forms.View.Details;
            // 
            // employeeIdColumn
            // 
            this.employeeIdColumn.Text = "Employee ID";
            this.employeeIdColumn.Width = 100;
            // 
            // employeeNameColumn
            // 
            this.employeeNameColumn.Text = "Name";
            this.employeeNameColumn.Width = 100;
            // 
            // employeePositionColumn
            // 
            this.employeePositionColumn.Text = "Position";
            this.employeePositionColumn.Width = 100;
            // 
            // employeeRoleColumn
            // 
            this.employeeRoleColumn.Text = "Role";
            this.employeeRoleColumn.Width = 100;
            // 
            // employeeShiftColumn
            // 
            this.employeeShiftColumn.Text = "Shift";
            this.employeeShiftColumn.Width = 100;
            // 
            // employeePhoneNumberColumn
            // 
            this.employeePhoneNumberColumn.Text = "Phone Number";
            this.employeePhoneNumberColumn.Width = 100;
            // 
            // employeeUsernameColumn
            // 
            this.employeeUsernameColumn.Text = "Username";
            this.employeeUsernameColumn.Width = 100;
            // 
            // employeePasswordColumn
            // 
            this.employeePasswordColumn.Text = "Password";
            this.employeePasswordColumn.Width = 100;
            // 
            // lvCustomers
            // 
            this.lvCustomers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.customerIdColumn,
            this.customerNameColumn,
            this.customerEmailColumn,
            this.customerPhoneNumberColumn,
            this.customerUsernameColumn,
            this.customerPasswordColumn});
            this.lvCustomers.FullRowSelect = true;
            this.lvCustomers.GridLines = true;
            this.lvCustomers.HideSelection = false;
            this.lvCustomers.Location = new System.Drawing.Point(12, 200);
            this.lvCustomers.Name = "lvCustomers";
            this.lvCustomers.Size = new System.Drawing.Size(700, 180);
            this.lvCustomers.TabIndex = 4;
            this.lvCustomers.UseCompatibleStateImageBehavior = false;
            this.lvCustomers.View = System.Windows.Forms.View.Details;
            //this.lvCustomers.SelectedIndexChanged += new System.EventHandler(this.lvCustomers_SelectedIndexChanged);
            // 
            // customerIdColumn
            // 
            this.customerIdColumn.Text = "Customer ID";
            this.customerIdColumn.Width = 100;
            // 
            // customerNameColumn
            // 
            this.customerNameColumn.Text = "Name";
            this.customerNameColumn.Width = 100;
            // 
            // customerEmailColumn
            // 
            this.customerEmailColumn.Text = "Email";
            this.customerEmailColumn.Width = 120;
            // 
            // customerPhoneNumberColumn
            // 
            this.customerPhoneNumberColumn.Text = "Phone Number";
            this.customerPhoneNumberColumn.Width = 100;
            // 
            // customerUsernameColumn
            // 
            this.customerUsernameColumn.Text = "Username";
            this.customerUsernameColumn.Width = 100;
            // 
            // customerPasswordColumn
            // 
            this.customerPasswordColumn.Text = "Password";
            this.customerPasswordColumn.Width = 100;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(881, 421);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(765, 420);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(530, 420);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AccountForm
            // 
            this.ClientSize = new System.Drawing.Size(986, 472);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnAddUser);
            this.Controls.Add(this.btnEditUser);
            this.Controls.Add(this.btnDeleteUser);
            this.Controls.Add(this.lvEmployees);
            this.Controls.Add(this.lvCustomers);
            this.Name = "AccountForm";
            this.Text = "Manage Accounts";
            this.Load += new System.EventHandler(this.AccountForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAddUser;
        private System.Windows.Forms.Button btnEditUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.ListView lvEmployees;
        private System.Windows.Forms.ListView lvCustomers;

        private System.Windows.Forms.ColumnHeader employeeIdColumn;
        private System.Windows.Forms.ColumnHeader employeeNameColumn;
        private System.Windows.Forms.ColumnHeader employeePositionColumn;
        private System.Windows.Forms.ColumnHeader employeeRoleColumn;
        private System.Windows.Forms.ColumnHeader employeeShiftColumn; // Added column for shift
        private System.Windows.Forms.ColumnHeader employeePhoneNumberColumn; // Added column for phone number
        private System.Windows.Forms.ColumnHeader employeeUsernameColumn;
        private System.Windows.Forms.ColumnHeader employeePasswordColumn;

        private System.Windows.Forms.ColumnHeader customerIdColumn;
        private System.Windows.Forms.ColumnHeader customerNameColumn;
        private System.Windows.Forms.ColumnHeader customerEmailColumn;
        private System.Windows.Forms.ColumnHeader customerPhoneNumberColumn;
        private System.Windows.Forms.ColumnHeader customerUsernameColumn;
        private System.Windows.Forms.ColumnHeader customerPasswordColumn;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnExit;
    }
}
