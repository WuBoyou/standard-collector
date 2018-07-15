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

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                EuropeStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public EuropeStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                EuropeStandardStruct standardInfo = new EuropeStandardStruct();

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
