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

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                AmericanStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public AmericanStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                AmericanStandardStruct standardInfo = new AmericanStandardStruct();

                standardInfo.Classification = match.Groups[1].Value;
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
