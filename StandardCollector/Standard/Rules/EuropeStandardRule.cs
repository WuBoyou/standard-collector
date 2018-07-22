using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class EuropeStandardRule : StandardRule
    {
        static EuropeStandardRule()
        {
            EuropeStandardRule.rules = new ReadOnlyCollection<EuropeStandardRule>(
                new EuropeStandardRule[]
                {
                    // EN 12663-2000
                    new EuropeStandardRule(@"^(EN)[ _]*(\d+)[-:](\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<EuropeStandardRule> rules;
        public static ReadOnlyCollection<EuropeStandardRule> Rules { get { return EuropeStandardRule.rules; } }

        private readonly string rule;

        public EuropeStandardRule(string rule)
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

                EuropeStandardStruct standardInfo = new EuropeStandardStruct(mark, number, year, name);

                return standardInfo;
            }

            return null;
        }
    }
}
