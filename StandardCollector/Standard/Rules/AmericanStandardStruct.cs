using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public struct AmericanStandardStruct
    {
        public string Classification;

        public string Number;

        public int Year;

        public string Name;

        public string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(Classification) || string.IsNullOrEmpty(Number))
                return string.Empty;

            return string.Format("{0} {1}-{2}", this.Classification, this.Number, this.Year);
        }

        public string GetFullName()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return this.GetStandardNumber();
            }

            return string.Format("{0} {1}", this.GetStandardNumber(), this.Name);
        }

        public override string ToString()
        {
            return this.GetFullName();
        }
    }
}
