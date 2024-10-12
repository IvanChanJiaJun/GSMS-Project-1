namespace GSMS_Project_1
{
    partial class AddUserForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblEmailOrPosition = new System.Windows.Forms.Label();
            this.txtEmailOrPosition = new System.Windows.Forms.TextBox();
            this.lblPhoneOrRole = new System.Windows.Forms.Label();
            this.txtPhoneOrRole = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();

            // New labels and textboxes for Shift and Phone Number for Employee
            this.lblShift = new System.Windows.Forms.Label();
            this.txtShift = new System.Windows.Forms.TextBox();
            this.lblEmployeePhone = new System.Windows.Forms.Label();
            this.txtEmployeePhone = new System.Windows.Forms.TextBox();

            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(150, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(102, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Add User";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(30, 70);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(150, 70);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 22);
            this.txtName.TabIndex = 2;
            // 
            // lblEmailOrPosition
            // 
            this.lblEmailOrPosition.AutoSize = true;
            this.lblEmailOrPosition.Location = new System.Drawing.Point(30, 120);
            this.lblEmailOrPosition.Name = "lblEmailOrPosition";
            this.lblEmailOrPosition.Size = new System.Drawing.Size(93, 16);
            this.lblEmailOrPosition.TabIndex = 3;
            this.lblEmailOrPosition.Text = "Email/Position";
            // 
            // txtEmailOrPosition
            // 
            this.txtEmailOrPosition.Location = new System.Drawing.Point(150, 120);
            this.txtEmailOrPosition.Name = "txtEmailOrPosition";
            this.txtEmailOrPosition.Size = new System.Drawing.Size(200, 22);
            this.txtEmailOrPosition.TabIndex = 4;
            // 
            // lblPhoneOrRole
            // 
            this.lblPhoneOrRole.AutoSize = true;
            this.lblPhoneOrRole.Location = new System.Drawing.Point(30, 170);
            this.lblPhoneOrRole.Name = "lblPhoneOrRole";
            this.lblPhoneOrRole.Size = new System.Drawing.Size(79, 16);
            this.lblPhoneOrRole.TabIndex = 5;
            this.lblPhoneOrRole.Text = "Phone/Role";
            // 
            // txtPhoneOrRole
            // 
            this.txtPhoneOrRole.Location = new System.Drawing.Point(150, 170);
            this.txtPhoneOrRole.Name = "txtPhoneOrRole";
            this.txtPhoneOrRole.Size = new System.Drawing.Size(200, 22);
            this.txtPhoneOrRole.TabIndex = 6;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(30, 220);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(70, 16);
            this.lblUsername.TabIndex = 7;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(150, 220);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 22);
            this.txtUsername.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 270);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 16);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(150, 270);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 22);
            this.txtPassword.TabIndex = 10;
            this.txtPassword.UseSystemPasswordChar = true;

            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(30, 320);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(37, 16);
            this.lblShift.TabIndex = 11;
            this.lblShift.Text = "Shift";
            // 
            // txtShift
            // 
            this.txtShift.Location = new System.Drawing.Point(150, 320);
            this.txtShift.Name = "txtShift";
            this.txtShift.Size = new System.Drawing.Size(200, 22);
            this.txtShift.TabIndex = 12;

            // 
            // lblEmployeePhone
            // 
            this.lblEmployeePhone.AutoSize = true;
            this.lblEmployeePhone.Location = new System.Drawing.Point(30, 370);
            this.lblEmployeePhone.Name = "lblEmployeePhone";
            this.lblEmployeePhone.Size = new System.Drawing.Size(105, 16);
            this.lblEmployeePhone.TabIndex = 13;
            this.lblEmployeePhone.Text = "Phone Number";
            // 
            // txtEmployeePhone
            // 
            this.txtEmployeePhone.Location = new System.Drawing.Point(150, 370);
            this.txtEmployeePhone.Name = "txtEmployeePhone";
            this.txtEmployeePhone.Size = new System.Drawing.Size(200, 22);
            this.txtEmployeePhone.TabIndex = 14;

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(150, 420);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // 
            // AddUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 500); // Adjusted form size to fit new fields
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmployeePhone);
            this.Controls.Add(this.lblEmployeePhone);
            this.Controls.Add(this.txtShift);
            this.Controls.Add(this.lblShift);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.txtPhoneOrRole);
            this.Controls.Add(this.lblPhoneOrRole);
            this.Controls.Add(this.txtEmailOrPosition);
            this.Controls.Add(this.lblEmailOrPosition);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblTitle);
            this.Name = "AddUserForm";
            this.Text = "Add User";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblEmailOrPosition;
        private System.Windows.Forms.TextBox txtEmailOrPosition;
        private System.Windows.Forms.Label lblPhoneOrRole;
        private System.Windows.Forms.TextBox txtPhoneOrRole;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;

        // New fields for Employee Shift and Phone Number
        private System.Windows.Forms.Label lblShift;
        private System.Windows.Forms.TextBox txtShift;
        private System.Windows.Forms.Label lblEmployeePhone;
        private System.Windows.Forms.TextBox txtEmployeePhone;

        private System.Windows.Forms.Button btnSave;
    }
}
