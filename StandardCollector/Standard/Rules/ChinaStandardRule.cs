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

        public override StandardInfo GetStandardInfo(string fullname)
        {
            try
            {
                ChinaStandardStruct standard = this.GetStandardStruct(fullname);
                return new StandardInfo(standard.GetStandardNumber(), standard.Name);
            }
            catch (Exception)
            {
                return null;
            }

        }

        public abstract ChinaStandardStruct GetStandardStruct(string fullname);
    }
}
