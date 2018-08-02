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
using Standard.Rules;
using Standard.Data;

namespace Standard.Test.UI.Winforms
{
    public partial class RetrievalDataForm : Form
    {
        private readonly Font boldFont = new Font("宋体", 9.07563f, FontStyle.Bold);
        
        private StandardDatabase database;

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
            if (this.database != null)
                this.database.Close();
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
            this.txtNewFileName.Size = new Size(this.tpStandardFile.Size.Width - 181, 25);
        }

        private void btnConnectDb_Click(object sender, EventArgs e)
        {
            DatabaseInfo dbInfo = new DatabaseInfo(this.txtServer.Text.Trim(),
                this.txtPort.Text.Trim(), this.txtDatabase.Text.Trim(),
                this.txtUser.Text.Trim(), this.txtPassword.Text.Trim());

            if (this.ConnectDatabase(dbInfo))
            {
                this.cbDatatables.Items.Clear();
                this.cbDatatables.Items.AddRange(this.GetTableNames(dbInfo.Database));
                FillTablesInfo();
            }

            this.btnConnectDb.Enabled = false;
        }

        private void btnDisconnectDb_Click(object sender, EventArgs e)
        {
            this.DisconnectDatabase();
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
            if (this.database == null)
                return;

            string standardNo = this.txtQueryStandardNo.Text.Trim();
            string standardName = this.txtQueryStandardName.Text.Trim();

            DataTable table = this.database.QueryStandardsInfo(standardNo, standardName);
            this.ShowTableRowsToListView(this.lstResult, table, this.ChangeStandardItemStyle);
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog standardDialog = this.CreateOpenStandardFileDialog();

            if (standardDialog.ShowDialog() == DialogResult.OK)
            {
                string fullName = standardDialog.FileName;
                string fileName = Path.GetFileName(fullName);

                this.txtFilePath.Text = fullName;
                this.txtFileName.Text = fileName;

                StandardParser parser = this.CreateStandardParserWithAllRules();
                StandardStruct standard = parser.Parse(Path.GetFileNameWithoutExtension(fileName));
                if (standard != null)
                {
                    this.txtExtractiveStandardNo.Text = standard.StandardNumber;
                    this.txtExtractiveStandardName.Text = standard.StandardName;

                    if (this.database != null)
                    {
                        DataTable table = this.database.QueryStandardsInfo(standard.StandardNumber, string.Empty);
                        this.ShowTableRowsToListView(this.lvMatched, table, null);
                    }
                }
            }
        }

        private StandardParser CreateStandardParserWithAllRules()
        {
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

            return parser;
        }

        private OpenFileDialog CreateOpenStandardFileDialog()
        {
            OpenFileDialog standardDialog = new OpenFileDialog();
            standardDialog.CheckFileExists = true;
            standardDialog.CheckPathExists = true;
            standardDialog.Title = "选择标准文件";
            standardDialog.SupportMultiDottedExtensions = false;
            standardDialog.AddExtension = true;
            standardDialog.Filter = "所有文件(*.*)|*.*";

            return standardDialog;
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
                else if(this.lvMatched.Items.Count == 1)
                {
                    ListViewItem item = this.lvMatched.Items[0];
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
            if (this.database == null)
            {
                this.database = StandardDatabase.Create(db);
                this.database.StateChange += Database_StateChange;
            }
            else
            {

                if (this.database.State == ConnectionState.Open)
                    return false;
                else
                    this.database.DatabaseInfo = db;
            }
            try
            {
                this.database.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private void DisconnectDatabase()
        {
            if (this.database == null)
                return;

            this.database.Close();
        }

        private void Database_StateChange(object sender, StateChangeEventArgs e)
        {
            if(e.CurrentState== ConnectionState.Open)
            {
                this.btnConnectDb.Enabled = false;
                this.btnDisconnectDb.Enabled = true;
            }
            else
            {
                this.btnConnectDb.Enabled = true;
                this.btnDisconnectDb.Enabled = false;
            }
        }

        private string[] GetTableNames(string database)
        {
            List<string> list = new List<string>();
            DataTable table = this.database.GetDatabaseSchema("", database, "", "BASE TABLE");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i]["TABLE_NAME"].ToString());
            }
            return list.ToArray();
        }

        private string[] GetColumnNames(string tableName)
        {
            List<string> list = new List<string>();
            DataTable table = this.database.GetTableSchema("", "", tableName, "");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(table.Rows[i]["COLUMN_NAME"].ToString());
            }
            return list.ToArray();
        }

        private void FillDatabaseInfo()
        {
            DataTable table = this.database.GetSchema();
            this.ShowTableToListView(this.lstDatatables, table);
        }

        private void FillTablesInfo()
        {
            DataTable table = this.database.GetDatabaseSchema(string.Empty, string.Empty, string.Empty, string.Empty);
            this.ShowTableToListView(this.lstDatatables, table);
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

            DataTable table = this.database.RetrieveTable(tableName, rowLimit);

            this.ShowTableToListView(this.lstDatatables, table);
        }
    }
}
