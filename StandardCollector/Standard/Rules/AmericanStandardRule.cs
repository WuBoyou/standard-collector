using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class AmericanStandardRule : StandardRule
    {

        static AmericanStandardRule()
        {
            AmericanStandardRule.rules = new ReadOnlyCollection<AmericanStandardRule>(
                new AmericanStandardRule[]
                {
                    // ASTM D2911-2010
                    new AmericanStandardRule(@"^(ASTM)[ _-]*([A-Z]\d{2,4})-(\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<AmericanStandardRule> rules;
        public static ReadOnlyCollection<AmericanStandardRule> Rules { get { return AmericanStandardRule.rules; } }

        private readonly string rule;

        private AmericanStandardRule()
        {
        }

        public AmericanStandardRule(string rule)
        {
            this.rule = rule;
        }

        protected string Rule { get { return this.rule; } }

        public override StandardStruct GetStandard(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                string classification = match.Groups[1].Value;
                string number = match.Groups[2].Value;
                int year = Int32.Parse(match.Groups[3].Value);
                string name = regex.Replace(fullname, String.Empty).Trim();

                DataVerification.CheckYear(year);

                AmericanStandardStruct standardInfo = new AmericanStandardStruct(classification, number, year, name);

                return standardInfo;
            }

            return null;
        }
    }
}
