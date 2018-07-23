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
    public partial class RetrievalDataForm : Form
    {
        private MySqlConnection connection;

        public RetrievalDataForm()
        {
            InitializeComponent();
            this.Resize += ReadDataForm_Resize;
            this.tpTables.SizeChanged += TpTables_SizeChanged;
            this.tpQuery.SizeChanged += TpQuery_SizeChanged;
            this.cbRowLimit.SelectedIndex = 0;

            this.Size = new Size(800, 600);
        }

        ~RetrievalDataForm()
        {
            if (this.connection != null)
            {
                this.connection.Close();
            }
        }

        private void ReadDataForm_Resize(object sender, EventArgs e)
        {
            this.tabQueryData.Size = new Size(this.Size.Width - 184, this.Size.Height - 71);
        }

        private void TpTables_SizeChanged(object sender, EventArgs e)
        {
            this.lstDatatables.Size = new Size(this.tpTables.Size.Width - 12, this.tpTables.Size.Height - 59);
        }

        private void TpQuery_SizeChanged(object sender, EventArgs e)
        {
            int offset = (this.tpQuery.Size.Width - 488) / 2;
            this.txtQueryStandardNo.Size = new Size(120 + offset, 25);
            this.label9.Location = new Point(202 + offset, 22);
            this.txtQueryStandardName.Location = new Point(275 + offset, 19);
            this.txtQueryStandardName.Size = new Size(120 + offset, 25);
            this.btnQueryStandards.Location = new Point(this.tpQuery.Size.Width - 84, 19);
            this.lstResult.Size = new Size(this.tpQuery.Size.Width - 18, this.tpQuery.Size.Height - 59);
        }

        private void btnConnectDb_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbInfo = new DatabaseInfo(this.txtServer.Text.Trim(),
                this.txtPort.Text.Trim(), this.txtDatabase.Text.Trim(),
                this.txtUser.Text.Trim(), this.txtPassword.Text.Trim());

            if (this.ConnectDatabase(dbInfo))
            {
                this.cbDatatables.Items.AddRange(this.GetTableNames(this.txtDatabase.Text));
                FillTablesInfo();
            }

            this.btnConnectDb.Enabled = false;
        }

        public bool ConnectDatabase(DatabaseInfo db)
        {
            if (this.connection == null)
                this.connection = new MySqlConnection();

            if (this.connection.State == ConnectionState.Open)
                return false;
            try
            {
                this.connection.ConnectionString = db.GetConnectionString();
                this.connection.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private string[] GetTableNames(string database)
        {
            List<string> list = new List<string>();
            DataTable table = this.GetDatabaseSchema("", database, "", "BASE TABLE");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i]["TABLE_NAME"].ToString());
            }
            return list.ToArray();
        }

        private string[] GetColumnNames(string tableName)
        {
            List<string> list = new List<string>();
            DataTable table = GetTableSchema("", "", tableName, "");
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
            DataTable table = this.GetDatabaseSchema(string.Empty, string.Empty, string.Empty, string.Empty);
            this.ShowTableToListView(table);
        }

        private DataTable GetDatabaseSchema(string catalog, string databaseName, string tableName, string tableType)
        {
            string[] restrictions = new string[4];
            restrictions[0] = catalog;                  // 表所在的Catalog
            restrictions[1] = databaseName;             // 表的所有者（数据库）
            restrictions[2] = tableName;                // 表的名字
            restrictions[3] = tableType;                // 表的类型
            DataTable table = this.connection.GetSchema("Tables", restrictions);
            return table;
        }

        private DataTable GetTableSchema(string catalog, string databaseName, string tableName, string columnName)
        {
            string[] restrictions = new string[4];
            restrictions[0] = catalog;                  // 列所在的Catalog
            restrictions[1] = databaseName;             // 列的所有者
            restrictions[2] = tableName;                // 列所在的表的名字
            restrictions[3] = columnName;               // 表示列名
            DataTable table = this.connection.GetSchema("Columns", restrictions);
            return table;
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
            string rowLimitText = this.cbRowLimit.SelectedItem as string;
            int rowLimit;
            if (!int.TryParse(rowLimitText, out rowLimit))
                rowLimit = -1;

            DataTable table = RetrieveTable(tableName, rowLimit);

            this.ShowTableToListView(table);
        }

        private DataTable RetrieveTable(string tableName, int rowLimit)
        {
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = string.Format("SELECT * FROM {0}{1}", tableName, this.GetRowLimitText(rowLimit));
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        private string GetRowLimitText(int rowLimit)
        {
            if (rowLimit == -1)
                return string.Empty;

            return string.Format(" LIMIT {0}", rowLimit);
        }

        private void btnQueryStandards_Click(object sender, EventArgs e)
        {
            if (this.connection == null || this.connection.State != ConnectionState.Open)
                return;

            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = "SELECT standardNo, standardName, englishName FROM standardlib WHERE (position(@std_no IN replace(standardNo,' ','')) > 0) AND (position(@std_name IN standardName) > 0) ORDER BY 1;";
            command.Parameters.Add(new MySqlParameter("@std_no", this.txtQueryStandardNo.Text.Trim()));
            command.Parameters.Add(new MySqlParameter("@std_name", this.txtQueryStandardName.Text.Trim()));
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            this.ShowTableRowsToListView(table);
        }

        private void ShowTableRowsToListView(DataTable table)
        {
            this.lstResult.BeginUpdate();

            this.lstResult.Items.Clear();

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
                this.lstResult.Items.Add(item);
            }

            for (int i = 0; i < this.lstResult.Columns.Count; i++)
            {
                this.lstResult.Columns[i].Width = -2;
            }
            this.lstResult.EndUpdate();
        }
    }
}
