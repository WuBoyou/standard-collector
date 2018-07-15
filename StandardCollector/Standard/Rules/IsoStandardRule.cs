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

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                IsoStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IsoStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                IsoStandardStruct standardInfo = new IsoStandardStruct();

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
