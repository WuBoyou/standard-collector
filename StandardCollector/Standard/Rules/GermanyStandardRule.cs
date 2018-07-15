using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class GermanyStandardRule : StandardRule
    {
        static GermanyStandardRule()
        {
            GermanyStandardRule.rules = new ReadOnlyCollection<GermanyStandardRule>(
                new GermanyStandardRule[]
                {
                    // DIN 4943-2013
                    new GermanyStandardRule(@"^(DIN)[ _]*(\d+)[-:](\d{4}|\d{2})"),
                    // DIN 6700-2-1997
                    new GermanyStandardRule(@"^(DIN)[ _]*(\d+-\d+)[-:](\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<GermanyStandardRule> rules;
        public static ReadOnlyCollection<GermanyStandardRule> Rules { get { return GermanyStandardRule.rules; } }

        private readonly string rule;

        public GermanyStandardRule(string rule)
        {
            this.rule = rule;
        }

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                GermanyStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public GermanyStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                GermanyStandardStruct standardInfo = new GermanyStandardStruct();

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
