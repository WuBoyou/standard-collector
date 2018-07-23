namespace Standard.Test.UI.Winforms
{
    partial class RetrievalDataForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConnectDb = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDatabase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabQueryData = new System.Windows.Forms.TabControl();
            this.tpTables = new System.Windows.Forms.TabPage();
            this.cbRowLimit = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstDatatables = new System.Windows.Forms.ListView();
            this.cbDatatables = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tpQuery = new System.Windows.Forms.TabPage();
            this.btnQueryStandards = new System.Windows.Forms.Button();
            this.lstResult = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtQueryStandardName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtQueryStandardNo = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.tabQueryData.SuspendLayout();
            this.tpTables.SuspendLayout();
            this.tpQuery.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnectDb);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtDatabase);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(136, 350);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据库信息";
            // 
            // btnConnectDb
            // 
            this.btnConnectDb.Location = new System.Drawing.Point(30, 312);
            this.btnConnectDb.Name = "btnConnectDb";
            this.btnConnectDb.Size = new System.Drawing.Size(75, 23);
            this.btnConnectDb.TabIndex = 10;
            this.btnConnectDb.Text = "连接";
            this.btnConnectDb.UseVisualStyleBackColor = true;
            this.btnConnectDb.Click += new System.EventHandler(this.btnConnectDb_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(9, 271);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(118, 25);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.Text = "root";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 253);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 8;
            this.label5.Text = "密码";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(9, 216);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(118, 25);
            this.txtUser.TabIndex = 7;
            this.txtUser.Text = "root";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 198);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "用户名";
            // 
            // txtDatabase
            // 
            this.txtDatabase.Location = new System.Drawing.Point(9, 160);
            this.txtDatabase.Name = "txtDatabase";
            this.txtDatabase.Size = new System.Drawing.Size(118, 25);
            this.txtDatabase.TabIndex = 5;
            this.txtDatabase.Text = "standards";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据库";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(9, 104);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(118, 25);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "3306";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "端口";
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(9, 48);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(118, 25);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "localhost";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务器";
            // 
            // tabQueryData
            // 
            this.tabQueryData.Controls.Add(this.tpTables);
            this.tabQueryData.Controls.Add(this.tpQuery);
            this.tabQueryData.Location = new System.Drawing.Point(154, 13);
            this.tabQueryData.Name = "tabQueryData";
            this.tabQueryData.SelectedIndex = 0;
            this.tabQueryData.Size = new System.Drawing.Size(496, 349);
            this.tabQueryData.TabIndex = 1;
            // 
            // tpTables
            // 
            this.tpTables.Controls.Add(this.cbRowLimit);
            this.tpTables.Controls.Add(this.label7);
            this.tpTables.Controls.Add(this.lstDatatables);
            this.tpTables.Controls.Add(this.cbDatatables);
            this.tpTables.Controls.Add(this.label6);
            this.tpTables.Location = new System.Drawing.Point(4, 25);
            this.tpTables.Name = "tpTables";
            this.tpTables.Padding = new System.Windows.Forms.Padding(3);
            this.tpTables.Size = new System.Drawing.Size(488, 320);
            this.tpTables.TabIndex = 0;
            this.tpTables.Text = "数据表";
            this.tpTables.UseVisualStyleBackColor = true;
            // 
            // cbRowLimit
            // 
            this.cbRowLimit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRowLimit.FormattingEnabled = true;
            this.cbRowLimit.Items.AddRange(new object[] {
            "无限制",
            "10",
            "50",
            "100",
            "200",
            "500",
            "1000",
            "2000"});
            this.cbRowLimit.Location = new System.Drawing.Point(317, 19);
            this.cbRowLimit.Name = "cbRowLimit";
            this.cbRowLimit.Size = new System.Drawing.Size(105, 23);
            this.cbRowLimit.TabIndex = 4;
            this.cbRowLimit.SelectedIndexChanged += new System.EventHandler(this.cbRowLimit_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(259, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "行限制";
            // 
            // lstDatatables
            // 
            this.lstDatatables.FullRowSelect = true;
            this.lstDatatables.GridLines = true;
            this.lstDatatables.HideSelection = false;
            this.lstDatatables.Location = new System.Drawing.Point(9, 48);
            this.lstDatatables.MultiSelect = false;
            this.lstDatatables.Name = "lstDatatables";
            this.lstDatatables.Size = new System.Drawing.Size(470, 261);
            this.lstDatatables.TabIndex = 2;
            this.lstDatatables.UseCompatibleStateImageBehavior = false;
            this.lstDatatables.View = System.Windows.Forms.View.Details;
            // 
            // cbDatatables
            // 
            this.cbDatatables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDatatables.FormattingEnabled = true;
            this.cbDatatables.Location = new System.Drawing.Point(64, 19);
            this.cbDatatables.Name = "cbDatatables";
            this.cbDatatables.Size = new System.Drawing.Size(180, 23);
            this.cbDatatables.TabIndex = 1;
            this.cbDatatables.SelectedIndexChanged += new System.EventHandler(this.cbDatatables_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "数据表";
            // 
            // tpQuery
            // 
            this.tpQuery.Controls.Add(this.btnQueryStandards);
            this.tpQuery.Controls.Add(this.lstResult);
            this.tpQuery.Controls.Add(this.txtQueryStandardName);
            this.tpQuery.Controls.Add(this.label9);
            this.tpQuery.Controls.Add(this.txtQueryStandardNo);
            this.tpQuery.Controls.Add(this.label8);
            this.tpQuery.Location = new System.Drawing.Point(4, 25);
            this.tpQuery.Name = "tpQuery";
            this.tpQuery.Padding = new System.Windows.Forms.Padding(3);
            this.tpQuery.Size = new System.Drawing.Size(488, 320);
            this.tpQuery.TabIndex = 1;
            this.tpQuery.Text = "查询";
            this.tpQuery.UseVisualStyleBackColor = true;
            // 
            // btnQueryStandards
            // 
            this.btnQueryStandards.Location = new System.Drawing.Point(404, 19);
            this.btnQueryStandards.Name = "btnQueryStandards";
            this.btnQueryStandards.Size = new System.Drawing.Size(75, 23);
            this.btnQueryStandards.TabIndex = 5;
            this.btnQueryStandards.Text = "查询";
            this.btnQueryStandards.UseVisualStyleBackColor = true;
            this.btnQueryStandards.Click += new System.EventHandler(this.btnQueryStandards_Click);
            // 
            // lstResult
            // 
            this.lstResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstResult.FullRowSelect = true;
            this.lstResult.GridLines = true;
            this.lstResult.HideSelection = false;
            this.lstResult.Location = new System.Drawing.Point(9, 48);
            this.lstResult.MultiSelect = false;
            this.lstResult.Name = "lstResult";
            this.lstResult.Size = new System.Drawing.Size(470, 261);
            this.lstResult.TabIndex = 4;
            this.lstResult.UseCompatibleStateImageBehavior = false;
            this.lstResult.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "标准编号";
            this.columnHeader1.Width = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "中文名称";
            this.columnHeader2.Width = 200;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "英文名称";
            this.columnHeader3.Width = 200;
            // 
            // txtQueryStandardName
            // 
            this.txtQueryStandardName.Location = new System.Drawing.Point(275, 19);
            this.txtQueryStandardName.Name = "txtQueryStandardName";
            this.txtQueryStandardName.Size = new System.Drawing.Size(120, 25);
            this.txtQueryStandardName.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(202, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 15);
            this.label9.TabIndex = 2;
            this.label9.Text = "标准名称";
            // 
            // txtQueryStandardNo
            // 
            this.txtQueryStandardNo.Location = new System.Drawing.Point(73, 19);
            this.txtQueryStandardNo.Name = "txtQueryStandardNo";
            this.txtQueryStandardNo.Size = new System.Drawing.Size(120, 25);
            this.txtQueryStandardNo.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "标准编号";
            // 
            // RetrievalDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(662, 374);
            this.Controls.Add(this.tabQueryData);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(620, 420);
            this.Name = "RetrievalDataForm";
            this.Text = "标准数据库查询系统";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabQueryData.ResumeLayout(false);
            this.tpTables.ResumeLayout(false);
            this.tpTables.PerformLayout();
            this.tpQuery.ResumeLayout(false);
            this.tpQuery.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabQueryData;
        private System.Windows.Forms.TabPage tpTables;
        private System.Windows.Forms.TabPage tpQuery;
        private System.Windows.Forms.ComboBox cbDatatables;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConnectDb;
        private System.Windows.Forms.ListView lstDatatables;
        private System.Windows.Forms.ComboBox cbRowLimit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQueryStandardName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtQueryStandardNo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListView lstResult;
        private System.Windows.Forms.Button btnQueryStandards;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
    }
}