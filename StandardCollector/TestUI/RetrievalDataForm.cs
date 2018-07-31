using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Standard.Rules;

namespace Standard.Test.UI.Winforms
{
    public partial class RetrievalDataForm : Form
    {
        private readonly Font boldFont = new Font("宋体", 9.07563f, FontStyle.Bold);

        private MySqlConnection connection;

        public RetrievalDataForm()
        {
            InitializeComponent();
            this.Resize += ReadDataForm_Resize;
            this.tpTables.SizeChanged += TpTables_SizeChanged;
            this.tpQuery.SizeChanged += TpQuery_SizeChanged;
            this.tpStandardFile.SizeChanged += TpStandardFile_SizeChanged;
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
            int offset = (this.tpQuery.Size.Width - 508) / 2;
            this.txtQueryStandardNo.Size = new Size(130 + offset, 25);
            this.label9.Location = new Point(212 + offset, 22);
            this.txtQueryStandardName.Location = new Point(285 + offset, 19);
            this.txtQueryStandardName.Size = new Size(130 + offset, 25);
            this.btnQueryStandards.Location = new Point(this.tpQuery.Size.Width - 84, 19);
            this.lstResult.Size = new Size(this.tpQuery.Size.Width - 18, this.tpQuery.Size.Height - 59);
        }

        private void TpStandardFile_SizeChanged(object sender, EventArgs e)
        {
            this.gbFileInfo.Size = new Size(this.tpStandardFile.Size.Width - 12, 88);
            this.txtFilePath.Size = new Size(this.gbFileInfo.Size.Width - 85, 25);
            this.txtFileName.Size = new Size(this.gbFileInfo.Size.Width - 166, 25);
            this.btnOpenFile.Location = new Point(this.gbFileInfo.Size.Width - 81, 51);

            this.gbStandardInfo.Size = new Size(this.tpStandardFile.Size.Width - 12, this.tpStandardFile.Size.Height - 180);
            int offset = (this.tpStandardFile.Size.Width - 508) / 2;
            this.txtExtractiveStandardNo.Size = new Size(160 + offset, 25);
            this.label13.Location = new Point(this.txtExtractiveStandardNo.Location.X + this.txtExtractiveStandardNo.Size.Width + 18, 22);
            this.txtExtractiveStandardName.Location = new Point(this.label13.Location.X + this.label13.Size.Width + 6, 19);
            this.txtExtractiveStandardName.Size = new Size(160 + offset, 25);
            this.lvMatched.Size = new Size(this.gbStandardInfo.Size.Width - 15, this.gbStandardInfo.Size.Height - 55);

            this.gbFileOperation.Size = new Size(this.tpStandardFile.Size.Width - 12, 68);
            this.gbFileOperation.Location = new Point(6, this.tpStandardFile.Size.Height - 74);
            this.txtNewFileName.Size = new Size(this.tpStandardFile.Size.Width - 169, 25);
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

        private void cbDatatables_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void cbRowLimit_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void btnQueryStandards_Click(object sender, EventArgs e)
        {
            if (this.connection == null || this.connection.State != ConnectionState.Open)
                return;

            string standardNo = this.txtQueryStandardNo.Text.Trim();
            string standardName = this.txtQueryStandardName.Text.Trim();

            DataTable table = this.QueryStandardsInfo(standardNo, standardName);
            this.ShowTableRowsToListView(this.lstResult, table, this.ChangeStandardItemStyle);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog standardDialog = new OpenFileDialog();
            standardDialog.CheckFileExists = true;
            standardDialog.CheckPathExists = true;
            standardDialog.Title = "选择标准文件";
            standardDialog.SupportMultiDottedExtensions = false;
            standardDialog.AddExtension = true;
            standardDialog.Filter = "所有文件(*.*)|*.*";

            if (standardDialog.ShowDialog() == DialogResult.OK)
            {
                this.txtFilePath.Text = standardDialog.FileName;
                this.txtFileName.Text = standardDialog.SafeFileName;

                StandardParser parser = new StandardParser();
                parser.RuleList.AddRange(ChinaVoluntaryStandardRule.Rules);
                parser.RuleList.AddRange(ChinaCompulsoryStandardRule.Rules);
                parser.RuleList.AddRange(AmericanStandardRule.Rules);
                parser.RuleList.AddRange(JapanStandardRule.Rules);
                parser.RuleList.AddRange(GermanyStandardRule.Rules);
                parser.RuleList.AddRange(EuropeStandardRule.Rules);
                parser.RuleList.AddRange(IsoStandardRule.Rules);
                parser.RuleList.AddRange(UicStandardRule.Rules);
                parser.RuleList.AddRange(IecStandardRule.Rules);

                StandardStruct standard = parser.Parse(Path.GetFileNameWithoutExtension(standardDialog.FileName));
                if (standard != null)
                {
                    this.txtExtractiveStandardNo.Text = standard.StandardNumber;
                    this.txtExtractiveStandardName.Text = standard.StandardName;

                    if (this.connection != null)
                    {
                        DataTable table = this.QueryStandardsInfo(standard.StandardNumber, string.Empty);
                        this.ShowTableRowsToListView(this.lvMatched, table, null);
                    }
                }
            }
        }

        private void btnFileRename_Click(object sender, EventArgs e)
        {
            if (this.lvMatched.Items.Count > 0)
            {
                if (this.lvMatched.SelectedItems.Count > 0)
                {
                    ListViewItem item = this.lvMatched.SelectedItems[0];
                    string standardNo = item.SubItems[0].Text;
                    string standardName = item.SubItems[1].Text;

                    string fileName = this.GetNewFileName(standardNo, standardName);
                    this.txtNewFileName.Text = fileName;
                }
            }
            else
            {
                string standardNo = this.txtExtractiveStandardNo.Text;
                string standardName = this.txtExtractiveStandardName.Text;

                string fileName = this.GetNewFileName(standardNo, standardName);
                this.txtNewFileName.Text = fileName;
            }
        }

        private string GetNewFileName(string standardNo, string standardName)
        {
            char[] standardNoChars = new char[standardNo.Length];
            for (int i = 0; i < standardNo.Length; i++)
            {
                if (standardNo[i] == ' ' || standardNo[i] == '/')
                    standardNoChars[i] = '_';
                else if (standardNo[i] == ':')
                    standardNoChars[i] = '-';
                else
                    standardNoChars[i] = standardNo[i];
            }

            char[] standardNameChars = new char[standardName.Length];
            for (int i = 0; i < standardName.Length; i++)
            {
                if (standardName[i] == ' ' || standardName[i] == '/')
                    standardNameChars[i] = '_';
                else
                    standardNameChars[i] = standardName[i];
            }

            StringBuilder result = new StringBuilder();
            result.Append(standardNoChars);
            result.Append('_');
            result.Append(standardNameChars);

            string fileName = result.ToString();
            return fileName;
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
            this.ShowTableToListView(this.lstDatatables, table);
        }

        private void FillTablesInfo()
        {
            DataTable table = this.GetDatabaseSchema(string.Empty, string.Empty, string.Empty, string.Empty);
            this.ShowTableToListView(this.lstDatatables, table);
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

        private void ShowTableToListView(ListView listView, DataTable table)
        {
            listView.BeginUpdate();

            listView.Columns.Clear();
            listView.Items.Clear();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                ColumnHeader column = new ColumnHeader();
                column.Text = table.Columns[i].Caption;
                listView.Columns.Add(column);
            }

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
                listView.Items.Add(item);
            }

            for (int i = 0; i < listView.Columns.Count; i++)
            {
                listView.Columns[i].Width = -2;
            }
            listView.EndUpdate();
        }

        private delegate void ChangeListViewItemDelegate(DataRow row, ListViewItem item);

        private void ShowTableRowsToListView(ListView listView, DataTable table, ChangeListViewItemDelegate handler)
        {
            listView.BeginUpdate();

            listView.Items.Clear();

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
                if (handler != null)
                    handler(row, item);

                listView.Items.Add(item);
            }

            for (int i = 0; i < listView.Columns.Count; i++)
            {
                listView.Columns[i].Width = -2;
            }
            listView.EndUpdate();
        }

        private void ChangeStandardItemStyle(DataRow row, ListViewItem item)
        {
            if ((string)row["state"] != "现行")
            {
                //item.Font = this.boldFont;
                item.ForeColor = Color.Gray;
            }
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

            this.ShowTableToListView(this.lstDatatables, table);
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

        private DataTable QueryStandardsInfo(string standardNo, string standardName)
        {
            MySqlCommand command = this.connection.CreateCommand();
            command.CommandText = @"SELECT standardNo, standardName, englishName, state FROM standardlib WHERE (position(replace(@std_no, ' ', '') IN replace(standardNo,' ','')) > 0) AND (position(@std_name IN standardName) > 0) 
UNION 
SELECT stdNo, name, englishName, state FROM csres_standard WHERE (position(replace(@std_no, ' ', '') IN replace(stdNo,' ','')) > 0) AND (position(@std_name IN name) > 0)     
ORDER BY 1;";
            command.Parameters.Add(new MySqlParameter("@std_no", standardNo));
            command.Parameters.Add(new MySqlParameter("@std_name", standardName));
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
    }
}
