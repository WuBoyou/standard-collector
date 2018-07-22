using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    /// <summary>推荐</summary>
    public sealed class ChinaVoluntaryStandardRule : ChinaStandardRule
    {

        static ChinaVoluntaryStandardRule()
        {
            ChinaVoluntaryStandardRule.rules = new ReadOnlyCollection<ChinaVoluntaryStandardRule>(
                new ChinaVoluntaryStandardRule[]
                {
                    // HG/T 2017-2011
                    new ChinaVoluntaryStandardRule(@"^(GB|AQ|BB|CB|CH|CJ|GJJ|CY|DA|DB|DL|DZ|EJ|FZ|GA|GJB|GY|HB|HG|HJ|HY|JB|JC|JG|JR|JT|JY|LB|LD|LY|MH|MT|MZ|NB|NY|QB|QC|QJ|QX|SB|SC|SH|SJ|SL|SN|SY|TB|TD|TY|WB|WH|WJ|WM|WS|XB|YB|YC|YD|YS|YY|YZ|DB11|DB12|DB13|DB14|DB15|DB21|DB22|DB23|DB31|DB32|DB33|DB34|DB35|DB36|DB37|DB41|DB42|DB43|DB44|DB45|DB46|DB50|DB51|DB52|DB53|DB54|DB61|DB62|DB63|DB64|DB65|CJJ)[ -]*(T|Z|/T|/Z|∕T|∕Z)[ _]*(\d+)-(\d{4}|\d{2})"),
                    // GB/Z21724.1-2008
                    new ChinaVoluntaryStandardRule(@"^(GB|AQ|BB|CB|CH|CJ|GJJ|CY|DA|DB|DL|DZ|EJ|FZ|GA|GJB|GY|HB|HG|HJ|HY|JB|JC|JG|JR|JT|JY|LB|LD|LY|MH|MT|MZ|NB|NY|QB|QC|QJ|QX|SB|SC|SH|SJ|SL|SN|SY|TB|TD|TY|WB|WH|WJ|WM|WS|XB|YB|YC|YD|YS|YY|YZ|DB11|DB12|DB13|DB14|DB15|DB21|DB22|DB23|DB31|DB32|DB33|DB34|DB35|DB36|DB37|DB41|DB42|DB43|DB44|DB45|DB46|DB50|DB51|DB52|DB53|DB54|DB61|DB62|DB63|DB64|DB65|CJJ)[ -]*(T|Z|/T|/Z|∕T|∕Z)[ _]*(\d+\.\d+)-(\d{4}|\d{2})"),
                    // GJB/Z 768A-1998
                    new ChinaVoluntaryStandardRule(@"^(GJB)[ -]*(Z|/Z)[ _]*(\d+[A]?)-(\d{4}|\d{2})")
                });

        }

        private static ReadOnlyCollection<ChinaVoluntaryStandardRule> rules;
        public static ReadOnlyCollection<ChinaVoluntaryStandardRule> Rules { get { return ChinaVoluntaryStandardRule.rules; } }


        public ChinaVoluntaryStandardRule(string rule) : base(rule)
        {
        }

        public override StandardStruct GetStandard(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(base.Rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                string prefix = match.Groups[1].Value;

                string type;
                string markString = match.Groups[2].Value.Trim().ToUpper();
                if (markString.Length > 0 && markString.Last() == 'T')
                    type = "/T";
                else if (markString.Length > 0 && markString.Last() == 'Z')
                    type = "/Z";
                else
                    type = String.Empty;

                string code = match.Groups[3].Value;
                int year = Int32.Parse(match.Groups[4].Value);
                string name = regex.Replace(fullname, String.Empty).Trim();

                DataVerification.CheckYear(year);

                ChinaStandardStruct standardInfo = new ChinaStandardStruct(prefix, type, code, year, name);

                return standardInfo;
            }

            return null;
        }
    }
}


// @"^(GB|TB|HG|FZ|GJB|JB|CJJ)[ _]*(/?T)?[ _]*(\d+\.?\d+)-(\d+)[ _]?([^\d|\s]+)";
// ^ 匹配字符串的开始位置

// 匹配中文字符
// \u4e00-\u9fa5

// 匹配简单的标准号+标准名
// (GB)[ _]*(\d+)-(\d{4}|\d{2})[ _]*([0-9a-zA-Z-/\u4e00-\u9fa5]+)
// (GB)[ _]*(\d+\.\d+)-(\d{4}|\d{2})[ _]*([0-9a-zA-Z-/]+)
// (GB)[ _]*(\d+\.\d+)-(\d{4}|\d{2})[ _]*([\u4e00-\u9fa5 0-9:：\(\)A-Z～]+)
// (GB)[ _]*(\d+)-(\d{4}|\d{2})[ _]*([\u4e00-\u9fa5 0-9:：\(\)A-Z～]+)
