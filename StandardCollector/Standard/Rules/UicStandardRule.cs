using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class UicStandardRule : StandardRule
    {
        static UicStandardRule()
        {
            UicStandardRule.rules = new ReadOnlyCollection<UicStandardRule>(
                new UicStandardRule[]
                {
                    // UIC 650-1983
                    new UicStandardRule(@"^(UIC)[ _]*(\d+)[-:](\d{4}|\d{2})"),
                    // UIC 515-4-1993
                    new UicStandardRule(@"^(UIC)[ _]*(\d+-\d+)[-:](\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<UicStandardRule> rules;
        public static ReadOnlyCollection<UicStandardRule> Rules { get { return UicStandardRule.rules; } }

        private readonly string rule;

        public UicStandardRule(string rule)
        {
            this.rule = rule;
        }

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                UicStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public UicStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                UicStandardStruct standardInfo = new UicStandardStruct();

                standardInfo.Mark = match.Groups[1].Value;
                standardInfo.Number = match.Groups[2].Value;
                standardInfo.Year = Int32.Parse(match.Groups[3].Value);
                standardInfo.Name = regex.Replace(fullname, String.Empty).Trim();

                DataVerification.CheckYear(standardInfo.Year);

                return standardInfo;
            }

            throw new ArgumentException("fullname");
        }
    }
}
