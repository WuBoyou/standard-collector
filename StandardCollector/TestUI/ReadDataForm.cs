using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Standard.Test.UI.Winforms
{
    public partial class ReadDataForm : Form
    {
        private MySqlConnection connection;

        public ReadDataForm()
        {
            InitializeComponent();
            this.Resize += ReadDataForm_Resize;
            this.tpTables.SizeChanged += TpTables_SizeChanged;
        }

        ~ReadDataForm()
        {
            if (this.connection != null)
            {
                this.connection.Close();
            }
        }

        private void ReadDataForm_Resize(object sender, EventArgs e)
        {
            this.tabData.Size = new Size(this.Size.Width - 184, this.Size.Height - 71);
        }

        private void TpTables_SizeChanged(object sender, EventArgs e)
        {
            this.lstDatatables.Size = new Size(this.tpTables.Size.Width - 15, this.tpTables.Size.Height - 59);
        }

        private void btnConnectDb_Click(object sender, EventArgs e)
        {
            if (this.connection == null)
            {
                this.connection = new MySqlConnection();
                this.connection.ConnectionString = string.Format(
                    "Data Source={0};Port={1};Database={2};User Id={3};Password={4}",
                    this.txtServer.Text.Trim(), this.txtPort.Text.Trim(), this.txtDatabase.Text.Trim(),
                    this.txtUser.Text.Trim(), this.txtPassword.Text.Trim());

                //if (!this.connection.Ping())
                //{
                //    MessageBox.Show("服务器不可用。", "连接数据库服务器", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    this.connection.Dispose();
                //    return;
                //}

                this.connection.Open();
                this.cbDatatables.Items.AddRange(this.GetTableNames(this.txtDatabase.Text));
                FillTablesInfo();

            }
            this.btnConnectDb.Enabled = false;
        }

        private string[] GetTableNames(string database)
        {
            List<string> list = new List<string>();
            string[] restrictions = new string[4];
            restrictions[0] = "";               // 表所在的Catalog
            restrictions[1] = database;         // 表的所有者
            restrictions[2] = "";               // 表的名字
            restrictions[3] = "BASE TABLE";     // 表的类型
            DataTable table = this.connection.GetSchema("Tables", restrictions);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i]["TABLE_NAME"].ToString());
            }
            return list.ToArray();
        }

        private string[] GetColumnNames(string tableName)
        {
            List<string> list = new List<string>();
            string[] restrictions = new string[4];
            restrictions[0] = "";               // 列所在的Catalog
            restrictions[1] = "";               // 列的所有者
            restrictions[2] = tableName;        // 列所在的表的名字
            restrictions[3] = "";               // 表示列名
            DataTable table = this.connection.GetSchema("Columns", restrictions);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i]["COLUMN_NAME"].ToString());
            }
            return list.ToArray();
        }

        private void FillDatabaseInfo()
        {
            DataTable table = this.connection.GetSchema();
            this.ShowTableToListView(table);
        }

        private void FillTablesInfo()
        {
            string[] restrictions = new string[4];
            restrictions[0] = "";               // 表所在的Catalog
            restrictions[1] = "";               // 表的所有者
            restrictions[2] = "";               // 表的名字
            restrictions[3] = "";               // 表的类型
            DataTable table = this.connection.GetSchema("Tables", restrictions);
            this.ShowTableToListView(table);
        }

        private void ShowTableToListView(DataTable table)
        {
            this.lstDatatables.Columns.Clear();
            this.lstDatatables.Items.Clear();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = table.Columns[i].Caption;
                this.lstDatatables.Columns.Add(column);
            }

            this.lstDatatables.BeginUpdate();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DataRow row = table.Rows[i];
                ListViewItem item = new ListViewItem();
                for (int j = 0; j < row.ItemArray.Length; j++)
                {
                    if (j == 0)
                        item.Text = row.ItemArray[j].ToString();
                    else
                        item.SubItems.Add(row.ItemArray[j].ToString());
                }
                this.lstDatatables.Items.Add(item);
            }

            for (int i = 0; i < this.lstDatatables.Columns.Count; i++)
            {
                this.lstDatatables.Columns[i].Width = -2;
            }
            this.lstDatatables.EndUpdate();
        }

        private void cbDatatables_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void cbRowLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void UpdateTable()
        {
            if (this.cbDatatables.SelectedIndex == -1)
            {
                return;
            }

            string tableName = this.cbDatatables.SelectedItem as string;
            string[] columnList = this.GetColumnNames(tableName);

            //string[] restrictions = new string[4];
            //restrictions[0] = "";               // 列所在的Catalog
            //restrictions[1] = "";               // 列的所有者
            //restrictions[2] = tableName;        // 列所在的表的名字
            //restrictions[3] = "";               // 表示列名
            //DataTable table = this.connection.GetSchema("Columns", restrictions);
            //this.ShowTableToListView(table);

            MySqlCommand command = this.connection.CreateCommand();
            string rowLimitText = this.GetRowLimitText();
            command.CommandText = string.Format("SELECT * FROM {0}{1}", tableName, rowLimitText);
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            this.ShowTableToListView(table);
        }

        private string GetRowLimitText()
        {
            if (this.cbRowLimit.SelectedIndex == -1)
                return string.Empty;

            if (this.cbRowLimit.SelectedItem as string == "无限制")
                return string.Empty;

            int limit = int.Parse(this.cbRowLimit.SelectedItem as string);
            return string.Format(" LIMIT {0}", limit);
        }
    }
}
