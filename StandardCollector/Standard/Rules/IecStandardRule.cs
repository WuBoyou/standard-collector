using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class IecStandardRule : StandardRule
    {
        static IecStandardRule()
        {
            IecStandardRule.rules = new ReadOnlyCollection<IsoStandardRule>(
                new IsoStandardRule[]
                {
                    // IEC 60571-2006
                    new IsoStandardRule(@"^(IEC)[ _]*(\d+)[-:](\d{4}|\d{2})"),
                });
        }

        private static ReadOnlyCollection<IsoStandardRule> rules;
        public static ReadOnlyCollection<IsoStandardRule> Rules { get { return IecStandardRule.rules; } }

        private readonly string rule;

        public IecStandardRule(string rule)
        {
            this.rule = rule;
        }

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                IecStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IecStandardStruct GetStandardStruct(string fullname)
        {
            fullname = TextProcessor.ReplaceUnderlineCharBySpaceCharacter(fullname);
            Regex regex = new Regex(this.rule);
            Match match = regex.Match(fullname);
            if (match.Success)
            {
                IecStandardStruct standardInfo = new IecStandardStruct();

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
