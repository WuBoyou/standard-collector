using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public class JapanStandardStruct : StandardStruct
    {
        /// <summary>日本标准起始标记（JIS）</summary>
        public string mark;

        /// <summary>标准分类标记</summary>
        public string classification;

        /// <summary>标准编号</summary>
        public string number;

        /// <summary>标准年份</summary>
        public int year;

        /// <summary>标准名称</summary>
        public string name;

        public JapanStandardStruct(string mark, string classification, string number, int year, string name)
        {
            this.mark = mark;
            this.classification = classification;
            this.number = number;
            this.year = year;
            this.name = name;
        }

        /// <summary>日本标准起始标记。</summary>
        /// <remarks>应始终为JIS。</remarks>
        public string Mark
        {
            get { return this.mark; }
        }

        /// <summary>标准分类标记。</summary>
        public string Classification
        {
            get { return this.classification; }
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
            if (string.IsNullOrEmpty(mark) || string.IsNullOrEmpty(classification) || string.IsNullOrEmpty(number))
                return string.Empty;

            if (this.year == 0)
                return string.Format("{0} {1}{2}", this.mark, this.classification, this.number);
            else
                return string.Format("{0} {1}{2}-{3}", this.mark, this.classification, this.number, this.year);
        }

        protected override string GetStandardName()
        {
            return this.name;
        }
    }
}


// JIS B2407-1995 O型密封圈的保护圈
// JIS B2706 ERRATUM 1-2001 蝶形弹簧（勘误1）
// JIS B3700-101-1996 工业自动化系统和集成电路.产品数据表示和信息交流.第101部分:综合应用方法:绘图
// JIS B3700-101 Technical Corrigendum 1-2002 工业自动化系统和继承.产品数据的表示和交换.第101部分:综合应用资源:绘图(技术勘误1)
// JIS B6016-1-1998 机创.润滑规程表示

