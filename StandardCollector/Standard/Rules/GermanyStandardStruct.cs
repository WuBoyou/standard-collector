using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class GermanyStandardStruct : StandardStruct
    {
        /// <summary>德国标准起始标记。应始终为DIN。</summary>
        private string mark;

        /// <summary>标准编号。</summary>
        private string number;

        /// <summary>标准年份。</summary>
        private int year;

        /// <summary>标准月份。</summary>
        private int month;

        /// <summary>标准名。</summary>
        private string name;

        public GermanyStandardStruct(string mark, string number, int year, string name)
        {
            this.mark = mark;
            this.number = number;
            this.year = year;
            this.month = 0;
            this.name = name;
        }

        public string Mark
        {
            get { return this.mark; }
        }

        public string Number
        {
            get { return this.number; }
        }

        public int Year
        {
            get { return this.year; }
        }

        public int Month
        {
            get { return this.month; }
        }

        protected override string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(mark) || string.IsNullOrEmpty(number))
                return string.Empty;

            if (this.year == 0)
                return string.Format("{0} {1}", this.mark, this.number);
            else
                if (this.month > 0 && this.month < 13)
                return string.Format("{0} {1}:{2}-{3}", this.mark, this.number, this.year, this.month);
            else
                return string.Format("{0} {1}:{2}", this.mark, this.number, this.year);
        }

        protected override string GetStandardName()
        {
            return this.name;
        }
    }
}
