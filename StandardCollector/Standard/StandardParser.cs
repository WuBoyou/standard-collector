using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Standard.Rules;

namespace Standard
{
    public sealed class StandardParser
    {
        private readonly List<StandardRule> rules;

        public StandardParser()
        {
            this.rules = new List<StandardRule>();
        }

        public List<StandardRule> RuleList
        {
            get { return this.rules; }
        }

        public StandardStruct Parse(string fullname)
        {
            foreach (var rule in this.rules)
            {
                try
                {
                    StandardStruct standard = rule.GetStandard(fullname);
                    if (standard != null)
                        return standard;
                    else
                        continue;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return null;
        }
    }
}
