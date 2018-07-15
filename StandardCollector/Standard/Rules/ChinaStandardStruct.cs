using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public struct ChinaStandardStruct
    {
        /// <summary>前缀。</summary>
        /// <remarks>表示标准分类，如国家标准、行业标准、地方标准。</remarks>
        public string Prefix;

        /// <summary>标准类型标记。</summary>
        /// <remarks>如为强制标准，则为空；如为推荐性标准，则为“/T”；如为指导性标准，则为“/Z”。</remarks>
        public string Mark;

        /// <summary>标准号。</summary>
        public string Number;

        /// <summary>标准年份。</summary>
        public int Year;

        /// <summary>标准名。</summary>
        public string Name;

        public string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(Prefix) || string.IsNullOrEmpty(Number))
                return string.Empty;

            return string.Format("{0}{1}{2}-{3}", this.Prefix, this.Mark, this.Number, this.Year);
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
