using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class IecStandardStruct : StandardStruct
    {
        /// <summary>国际电工委员会标准起始标记。应始终为IEC。</summary>
        public string mark;

        /// <summary>标准编号</summary>
        public string number;

        /// <summary>标准年份</summary>
        public int year;

        /// <summary>标准名称</summary>
        public string name;

        public IecStandardStruct(string mark, string number, int year, string name)
        {
            this.mark = mark;
            this.number = number;
            this.year = year;
            this.name = name;
        }

        /// <summary>国际电工委员会标准起始标记。应始终为IEC。</summary>
        public string Mark
        {
            get { return this.mark; }
        }

        /// <summary>标准编号。</summary>
        public string Number
        {
            get { return this.number; }
        }

        /// <summary>标准年份。</summary>
        public int Year
        {
            get { return this.year; }
        }

        protected override string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(mark) || string.IsNullOrEmpty(number))
                return string.Empty;

            if (this.year == 0)
                return string.Format("{0} {1}", this.mark, this.number);
            else
                return string.Format("{0} {1}:{2}", this.mark, this.number, this.year);
        }

        protected override string GetStandardName()
        {
            return this.name;
        }
    }
}
