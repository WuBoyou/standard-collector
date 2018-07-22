using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class IsoStandardRule : StandardRule
    {
        static IsoStandardRule()
        {
            IsoStandardRule.rules = new ReadOnlyCollection<IsoStandardRule>(
                new IsoStandardRule[]
                {
                    // ISO 9001-2008
                    new IsoStandardRule(@"^(ISO)[ _]*(\d+)[-:](\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<IsoStandardRule> rules;
        public static ReadOnlyCollection<IsoStandardRule> Rules { get { return IsoStandardRule.rules; } }

        private readonly string rule;

        public IsoStandardRule(string rule)
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

                IsoStandardStruct standardInfo = new IsoStandardStruct(mark, number, year, name);

                return standardInfo;
            }

            return null;
        }
    }
}
