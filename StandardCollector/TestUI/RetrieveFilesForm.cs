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

namespace Standard.Test.UI.Winforms
{
    public partial class RetrieveFilesForm : Form
    {
        public RetrieveFilesForm()
        {
            InitializeComponent();

            this.SizeChanged += TestForm_SizeChanged;
        }

        private void TestForm_SizeChanged(object sender, EventArgs e)
        {
            this.lstFileList.Size = new Size(this.Size.Width - 54, this.Size.Height - 148);
            this.btnChangePath.Location = new Point(this.Size.Width - 134, 28);
            this.txtPath.Size = new Size(this.Size.Width - 221, 21);
        }

        private void btnRetrieve_Click(object sender, EventArgs e)
        {
            this.lstFileList.Items.Clear();

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

            StandardFileEnumerator fileEnumerator = new StandardFileEnumerator(this.txtPath.Text);
            foreach (var file in fileEnumerator)
            {
                ListViewItem item = this.CreateItem(file);
                this.lstFileList.Items.Add(item);

                string fileName = Path.GetFileNameWithoutExtension(file);
                StandardStruct standard = parser.Parse(fileName);
                if (standard != null)
                {
                    item.SubItems[1].Text = standard.StandardNumber;
                    item.SubItems[2].Text = standard.StandardName;
                }
            }
        }

        private ListViewItem CreateItem(string filename)
        {
            ListViewItem item = new ListViewItem(filename);

            item.SubItems.Add(String.Empty);
            item.SubItems.Add(String.Empty);

            return item;
        }

        private void btnChangePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "选择包含标准文件的文件夹。";
            dialog.ShowNewFolderButton = false;
            dialog.SelectedPath = this.txtPath.Text;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
        }

        private void btnStore_Click(object sender, EventArgs e)
        {

        }

        private void btnXmlDatabase_Click(object sender, EventArgs e)
        {

        }
    }
}

