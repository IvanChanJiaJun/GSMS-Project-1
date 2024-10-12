using System.Windows.Forms;

namespace GSMS_Project_1
{
    partial class EmployeeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelEmployeeName = new System.Windows.Forms.Label();
            this.lvOrders = new System.Windows.Forms.ListView();
            this.orderIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.customerIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderDateColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.orderStatusColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.employeeIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvOrderDetails = new System.Windows.Forms.ListView();
            this.productIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.quantityColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lvProducts = new System.Windows.Forms.ListView();
            this.productListIdColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productListNameColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productListPriceColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.productListStockLevelColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.btnEditItem = new System.Windows.Forms.Button();
            this.btnDeleteItem = new System.Windows.Forms.Button();
            this.btnDeleteOrder = new System.Windows.Forms.Button();
            this.lblOrders = new System.Windows.Forms.Label();
            this.lblOrderDetails = new System.Windows.Forms.Label();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.cmbOrderStatus = new System.Windows.Forms.ComboBox();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelEmployeeName
            // 
            this.labelEmployeeName.AutoSize = true;
            this.labelEmployeeName.Location = new System.Drawing.Point(30, 31);
            this.labelEmployeeName.Name = "labelEmployeeName";
            this.labelEmployeeName.Size = new System.Drawing.Size(44, 16);
            this.labelEmployeeName.TabIndex = 0;
            this.labelEmployeeName.Text = "label1";
            // 
            // lvOrders
            // 
            this.lvOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.orderIdColumn,
            this.customerIdColumn,
            this.orderDateColumn,
            this.orderStatusColumn,
            this.employeeIdColumn});
            this.lvOrders.FullRowSelect = true;
            this.lvOrders.GridLines = true;
            this.lvOrders.HideSelection = false;
            this.lvOrders.Location = new System.Drawing.Point(12, 70);
            this.lvOrders.Name = "lvOrders";
            this.lvOrders.Size = new System.Drawing.Size(600, 200);
            this.lvOrders.TabIndex = 1;
            this.lvOrders.UseCompatibleStateImageBehavior = false;
            this.lvOrders.View = System.Windows.Forms.View.Details;
            this.lvOrders.SelectedIndexChanged += new System.EventHandler(this.lvOrders_SelectedIndexChanged);
            // 
            // orderIdColumn
            // 
            this.orderIdColumn.Text = "Order ID";
            this.orderIdColumn.Width = 70;
            // 
            // customerIdColumn
            // 
            this.customerIdColumn.Text = "Customer ID";
            this.customerIdColumn.Width = 70;
            // 
            // orderDateColumn
            // 
            this.orderDateColumn.Text = "Order Date";
            this.orderDateColumn.Width = 100;
            // 
            // orderStatusColumn
            // 
            this.orderStatusColumn.Text = "Order Status";
            this.orderStatusColumn.Width = 80;
            // 
            // employeeIdColumn
            // 
            this.employeeIdColumn.Text = "Employee ID";
            this.employeeIdColumn.Width = 80;
            // 
            // lvOrderDetails
            // 
            this.lvOrderDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.productIdColumn,
            this.productNameColumn,
            this.quantityColumn});
            this.lvOrderDetails.FullRowSelect = true;
            this.lvOrderDetails.GridLines = true;
            this.lvOrderDetails.HideSelection = false;
            this.lvOrderDetails.Location = new System.Drawing.Point(12, 320);
            this.lvOrderDetails.Name = "lvOrderDetails";
            this.lvOrderDetails.Size = new System.Drawing.Size(600, 200);
            this.lvOrderDetails.TabIndex = 2;
            this.lvOrderDetails.UseCompatibleStateImageBehavior = false;
            this.lvOrderDetails.View = System.Windows.Forms.View.Details;
            this.lvOrderDetails.SelectedIndexChanged += new System.EventHandler(this.lvOrderDetails_SelectedIndexChanged);
            // 
            // productIdColumn
            // 
            this.productIdColumn.Text = "Product ID";
            this.productIdColumn.Width = 100;
            // 
            // productNameColumn
            // 
            this.productNameColumn.Text = "Product Name";
            this.productNameColumn.Width = 100;
            // 
            // quantityColumn
            // 
            this.quantityColumn.Text = "Quantity";
            this.quantityColumn.Width = 100;
            // 
            // lvProducts
            // 
            this.lvProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.productListIdColumn,
            this.productListNameColumn,
            this.productListPriceColumn,
            this.productListStockLevelColumn});
            this.lvProducts.FullRowSelect = true;
            this.lvProducts.GridLines = true;
            this.lvProducts.HideSelection = false;
            this.lvProducts.Location = new System.Drawing.Point(650, 70);
            this.lvProducts.Name = "lvProducts";
            this.lvProducts.Size = new System.Drawing.Size(500, 200);
            this.lvProducts.TabIndex = 9;
            this.lvProducts.UseCompatibleStateImageBehavior = false;
            this.lvProducts.View = System.Windows.Forms.View.Details;
            // 
            // productListIdColumn
            // 
            this.productListIdColumn.Text = "Product ID";
            this.productListIdColumn.Width = 100;
            // 
            // productListNameColumn
            // 
            this.productListNameColumn.Text = "Product Name";
            this.productListNameColumn.Width = 120;
            // 
            // productListPriceColumn
            // 
            this.productListPriceColumn.Text = "Price";
            this.productListPriceColumn.Width = 50;
            // 
            // productListStockLevelColumn
            // 
            this.productListStockLevelColumn.Text = "Stock Level";
            this.productListStockLevelColumn.Width = 80;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(650, 294);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 22);
            this.txtQuantity.TabIndex = 10;
            // 
            // btnAddItem
            // 
            this.btnAddItem.Location = new System.Drawing.Point(650, 342);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(100, 23);
            this.btnAddItem.TabIndex = 3;
            this.btnAddItem.Text = "Add Item";
            this.btnAddItem.UseVisualStyleBackColor = true;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // btnEditItem
            // 
            this.btnEditItem.Location = new System.Drawing.Point(650, 444);
            this.btnEditItem.Name = "btnEditItem";
            this.btnEditItem.Size = new System.Drawing.Size(100, 23);
            this.btnEditItem.TabIndex = 4;
            this.btnEditItem.Text = "Edit Item";
            this.btnEditItem.UseVisualStyleBackColor = true;
            this.btnEditItem.Click += new System.EventHandler(this.btnEditItem_Click_1);
            // 
            // btnDeleteItem
            // 
            this.btnDeleteItem.Location = new System.Drawing.Point(650, 389);
            this.btnDeleteItem.Name = "btnDeleteItem";
            this.btnDeleteItem.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteItem.TabIndex = 5;
            this.btnDeleteItem.Text = "Delete Item";
            this.btnDeleteItem.UseVisualStyleBackColor = true;
            this.btnDeleteItem.Click += new System.EventHandler(this.btnDeleteItem_Click_1);
            // 
            // btnDeleteOrder
            // 
            this.btnDeleteOrder.Location = new System.Drawing.Point(650, 497);
            this.btnDeleteOrder.Name = "btnDeleteOrder";
            this.btnDeleteOrder.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteOrder.TabIndex = 6;
            this.btnDeleteOrder.Text = "Delete Order";
            this.btnDeleteOrder.UseVisualStyleBackColor = true;
            this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click_1);
            // 
            // lblOrders
            // 
            this.lblOrders.AutoSize = true;
            this.lblOrders.Location = new System.Drawing.Point(12, 50);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(48, 16);
            this.lblOrders.TabIndex = 7;
            this.lblOrders.Text = "Orders";
            // 
            // lblOrderDetails
            // 
            this.lblOrderDetails.AutoSize = true;
            this.lblOrderDetails.Location = new System.Drawing.Point(12, 300);
            this.lblOrderDetails.Name = "lblOrderDetails";
            this.lblOrderDetails.Size = new System.Drawing.Size(86, 16);
            this.lblOrderDetails.TabIndex = 8;
            this.lblOrderDetails.Text = "Order Details";
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Location = new System.Drawing.Point(528, 527);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(44, 16);
            this.lblTotalPrice.TabIndex = 11;
            this.lblTotalPrice.Text = "label1";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(1186, 519);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 12;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click_1);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1091, 519);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 13;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click_1);
            // 
            // cmbOrderStatus
            // 
            this.cmbOrderStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOrderStatus.FormattingEnabled = true;
            this.cmbOrderStatus.Items.AddRange(new object[] {
            "Pending",
            "Completed"});
            this.cmbOrderStatus.Location = new System.Drawing.Point(827, 297);
            this.cmbOrderStatus.Name = "cmbOrderStatus";
            this.cmbOrderStatus.Size = new System.Drawing.Size(121, 24);
            this.cmbOrderStatus.TabIndex = 14;
            // 
            // btnUpdateStatus
            // 
            this.btnUpdateStatus.Location = new System.Drawing.Point(827, 337);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(121, 23);
            this.btnUpdateStatus.TabIndex = 15;
            this.btnUpdateStatus.Text = "Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = true;
            this.btnUpdateStatus.Click += new System.EventHandler(this.btnUpdateStatus_Click_1);
            // 
            // EmployeeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1286, 600);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblTotalPrice);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.lvProducts);
            this.Controls.Add(this.lblOrderDetails);
            this.Controls.Add(this.lblOrders);
            this.Controls.Add(this.btnDeleteOrder);
            this.Controls.Add(this.btnDeleteItem);
            this.Controls.Add(this.btnEditItem);
            this.Controls.Add(this.btnAddItem);
            this.Controls.Add(this.lvOrderDetails);
            this.Controls.Add(this.lvOrders);
            this.Controls.Add(this.labelEmployeeName);
            this.Controls.Add(this.cmbOrderStatus);
            this.Controls.Add(this.btnUpdateStatus);
            this.Name = "EmployeeForm";
            this.Text = "Employee Order Management";
            this.Load += new System.EventHandler(this.EmployeeForm_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelEmployeeName;
        private System.Windows.Forms.ListView lvOrders;
        private System.Windows.Forms.ColumnHeader orderIdColumn;
        private System.Windows.Forms.ColumnHeader customerIdColumn;
        private System.Windows.Forms.ColumnHeader orderDateColumn;
        private System.Windows.Forms.ColumnHeader orderStatusColumn;
        private ColumnHeader employeeIdColumn;
        private System.Windows.Forms.ListView lvOrderDetails;
        private System.Windows.Forms.ColumnHeader productIdColumn;
        private System.Windows.Forms.ColumnHeader productNameColumn;
        private System.Windows.Forms.ColumnHeader quantityColumn;
        private System.Windows.Forms.ListView lvProducts;
        private System.Windows.Forms.ColumnHeader productListIdColumn;
        private System.Windows.Forms.ColumnHeader productListNameColumn;
        private System.Windows.Forms.ColumnHeader productListPriceColumn;
        private System.Windows.Forms.ColumnHeader productListStockLevelColumn;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.Button btnEditItem;
        private System.Windows.Forms.Button btnDeleteItem;
        private System.Windows.Forms.Button btnDeleteOrder;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Label lblOrderDetails;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.ComboBox cmbOrderStatus; // ComboBox for selecting order status
        private System.Windows.Forms.Button btnUpdateStatus; // Button to update order status
    }
}
