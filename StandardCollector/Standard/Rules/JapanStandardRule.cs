using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class JapanStandardRule : StandardRule
    {

        static JapanStandardRule()
        {
            JapanStandardRule.rules = new ReadOnlyCollection<JapanStandardRule>(
                new JapanStandardRule[]
                {
                    // JIS L4006-1998
                    new JapanStandardRule(@"^(JIS)[ _-]*([A-Z])[ _-]*(\d+)-(\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<JapanStandardRule> rules;
        public static ReadOnlyCollection<JapanStandardRule> Rules { get { return JapanStandardRule.rules; } }

        private readonly string rule;

        private JapanStandardRule()
        {
        }

        public JapanStandardRule(string rule)
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
                string mark = match.Groups[1].Value;
                string classification = match.Groups[2].Value;
                string number = match.Groups[3].Value;
                int year = Int32.Parse(match.Groups[4].Value);
                string name = regex.Replace(fullname, String.Empty).Trim();

                DataVerification.CheckYear(year);

                JapanStandardStruct standardInfo = new JapanStandardStruct(mark, classification, number, year, name);

                return standardInfo;
            }

            return null;
        }
    }
}
