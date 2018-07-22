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

        public override StandardStruct GetStandard(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                string mark = match.Groups[1].Value;
                string number = match.Groups[2].Value;
                int year = Int32.Parse(match.Groups[3].Value);
                string name = regex.Replace(fullname, String.Empty).Trim();

                DataVerification.CheckYear(year);

                GermanyStandardStruct standardInfo = new GermanyStandardStruct(mark, number, year, name);

                return standardInfo;
            }

            return null;
        }
    }
}
