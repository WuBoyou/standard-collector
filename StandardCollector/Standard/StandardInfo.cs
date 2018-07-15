using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard
{
    public class StandardInfo
    {
        private string number;                                  // 标准编号
        private string name;                                    // 标准名称
        private string state;                                   // 标准状态
        private string overview;                                // 标准简介
        private string englishName;                             // 英文名称
        private string replacedStandardNumber;                  // 替代情况
        private string chineseClassificationForStandards;       // 中标分类
        private string internationalClassificationForStandards; // ICS分类
        private string udc;                                     // UDC分类
        private string adoptedInternationalStandard;            // 采标情况
        private string publisher;                               // 发布部门
        private string issuanceDateString;                      // 发布日期
        private string executeDateString;                       // 实施日期
        private string revocatoryDateString;                    // 作废日期
        private string firstIssuanceDateString;                 // 首发日期
        private string reviewDateString;                        // 复审日期
        private string sponsor;                                 // 提出单位
        private string technicalCommittees;                     // 归口单位
        private string governor;                                // 主管部门
        private string draftingCommittee;                       // 起草单位

        private string pagesString;                             // 页数
        private string press;                                   // 出版社
        private string publicationDateString;                   // 出版日期

        internal StandardInfo()
        {
            this.name = String.Empty;
            this.number = String.Empty;
            this.state = String.Empty;
            this.overview = String.Empty;
            this.englishName = String.Empty;
            this.replacedStandardNumber = String.Empty;
            this.chineseClassificationForStandards = String.Empty;
            this.internationalClassificationForStandards = String.Empty;
            this.udc = String.Empty;
            this.adoptedInternationalStandard = String.Empty;
            this.publisher = String.Empty;
            this.issuanceDateString = String.Empty;
            this.executeDateString = String.Empty;
            this.revocatoryDateString = String.Empty;
            this.firstIssuanceDateString = String.Empty;
            this.reviewDateString = String.Empty;
            this.sponsor = String.Empty;
            this.technicalCommittees = String.Empty;
            this.governor = String.Empty;
            this.draftingCommittee = String.Empty;

            this.pagesString = String.Empty;
            this.press = String.Empty;
            this.publicationDateString = String.Empty;
        }

        public StandardInfo(string number, string name)
        {
            this.number = number;
            this.name = name;
        }

        public string Number
        {
            get { return this.number; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public override string ToString()
        {
            return String.Format("{0} {1}", this.number, this.name);
        }
    }
}
