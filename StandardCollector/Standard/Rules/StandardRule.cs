using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public abstract class StandardRule
    {
        public abstract StandardInfo GetStandardInfo(string fullname);
    }
}
