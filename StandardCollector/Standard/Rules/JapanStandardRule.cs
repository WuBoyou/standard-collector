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

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                JapanStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public JapanStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                JapanStandardStruct standardInfo = new JapanStandardStruct();

                standardInfo.Mark = match.Groups[1].Value;
                standardInfo.Classification = match.Groups[2].Value;
                standardInfo.Number = match.Groups[3].Value;
                standardInfo.Year = Int32.Parse(match.Groups[4].Value);
                standardInfo.Name = regex.Replace(fullname, String.Empty).Trim();

                DataVerification.CheckYear(standardInfo.Year);

                return standardInfo;
            }

            throw new ArgumentException("fullname");
        }
    }
}
