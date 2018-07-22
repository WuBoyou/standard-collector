namespace Standard.Test.UI.Winforms
{
    partial class ReadDataForm
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
            this.tabData = new System.Windows.Forms.TabControl();
            this.tpTables = new System.Windows.Forms.TabPage();
            this.cbRowLimit = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lstDatatables = new System.Windows.Forms.ListView();
            this.cbDatatables = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.tabData.SuspendLayout();
            this.tpTables.SuspendLayout();
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
            // tabData
            // 
            this.tabData.Controls.Add(this.tpTables);
            this.tabData.Controls.Add(this.tabPage2);
            this.tabData.Location = new System.Drawing.Point(154, 13);
            this.tabData.Name = "tabData";
            this.tabData.SelectedIndex = 0;
            this.tabData.Size = new System.Drawing.Size(436, 349);
            this.tabData.TabIndex = 1;
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
            this.tpTables.Size = new System.Drawing.Size(428, 320);
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
            this.lstDatatables.Size = new System.Drawing.Size(413, 261);
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
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(428, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "应用";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ReadDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 374);
            this.Controls.Add(this.tabData);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(620, 420);
            this.Name = "ReadDataForm";
            this.Text = "读取数据库";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabData.ResumeLayout(false);
            this.tpTables.ResumeLayout(false);
            this.tpTables.PerformLayout();
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
        private System.Windows.Forms.TabControl tabData;
        private System.Windows.Forms.TabPage tpTables;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cbDatatables;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnConnectDb;
        private System.Windows.Forms.ListView lstDatatables;
        private System.Windows.Forms.ComboBox cbRowLimit;
        private System.Windows.Forms.Label label7;
    }
}