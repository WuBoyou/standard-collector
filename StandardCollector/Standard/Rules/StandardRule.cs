using System;

namespace Standard.Rules
{
    public abstract class StandardRule
    {
        public abstract StandardStruct GetStandard(string fullname);
    }
}
