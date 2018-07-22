using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public abstract class ChinaStandardRule : StandardRule
    {
        private readonly string rule;

        private ChinaStandardRule()
        {
        }

        public ChinaStandardRule(string rule)
        {
            this.rule = rule;
        }

        protected string Rule { get { return this.rule; } }
    }
}
