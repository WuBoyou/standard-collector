using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public struct GermanyStandardStruct
    {
        /// <summary>德国标准起始标记。应始终为DIN。</summary>
        public string Mark;

        /// <summary>标准号。</summary>
        public string Number;

        /// <summary>标准年份。</summary>
        public int Year;

        /// <summary>标准月份。</summary>
        public int Month;

        /// <summary>标准名。</summary>
        public string Name;

        public string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(Mark) || string.IsNullOrEmpty(Number))
                return string.Empty;

            if (this.Year == 0)
                return string.Format("{0} {1}", this.Mark, this.Number);
            else
                if (this.Month > 0 && this.Month < 13)
                return string.Format("{0} {1}:{2}-{3}", this.Mark, this.Number, this.Year, this.Month);
            else
                return string.Format("{0} {1}:{2}", this.Mark, this.Number, this.Year);
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
