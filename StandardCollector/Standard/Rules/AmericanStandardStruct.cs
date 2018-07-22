using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class AmericanStandardStruct : StandardStruct
    {
        /// <summary>标准前缀（ASTM）</summary>
        private string classification;
        /// <summary>标准编号</summary>
        private string number;
        /// <summary>标准发布年份</summary>
        private int year;
        /// <summary>标准名称</summary>
        private string name;

        public AmericanStandardStruct(string classification, string number, int year, string name)
        {
            this.classification = classification;
            this.number = number;
            this.year = year;
            this.name = name;
        }

        /// <summary>标准前缀。</summary>
        /// <remarks>应始终为ASTM。</remarks>
        public string Classification
        {
            get { return this.classification; }
        }

        /// <summary>标准编号。</summary>
        public string Number
        {
            get { return this.number; }
        }

        /// <summary>标准发布年份。</summary>
        public int Year
        {
            get { return this.year; }
        }

        protected override string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(classification) || string.IsNullOrEmpty(number))
                return string.Empty;

            return string.Format("{0} {1}-{2}", this.classification, this.number, this.year);
        }

        protected override string GetStandardName()
        {
            return this.name;
        }
    }
}
