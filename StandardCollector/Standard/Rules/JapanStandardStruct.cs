using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    public struct JapanStandardStruct
    {
        /// <summary>日本标准起始标记。应始终为JIS。</summary>
        public string Mark;

        /// <summary>标准分类标记。</summary>
        public string Classification;

        /// <summary>标准号。</summary>
        public string Number;

        /// <summary>标准年份。</summary>
        public int Year;

        /// <summary>标准名。</summary>
        public string Name;

        public string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(Mark) || string.IsNullOrEmpty(Classification) || string.IsNullOrEmpty(Number))
                return string.Empty;

            if (this.Year == 0)
                return string.Format("{0} {1}{2}", this.Mark, this.Classification, this.Number);
            else
                return string.Format("{0} {1}{2}-{3}", this.Mark, this.Classification, this.Number, this.Year);
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


// JIS B2407-1995 O型密封圈的保护圈
// JIS B2706 ERRATUM 1-2001 蝶形弹簧（勘误1）
// JIS B3700-101-1996 工业自动化系统和集成电路.产品数据表示和信息交流.第101部分:综合应用方法:绘图
// JIS B3700-101 Technical Corrigendum 1-2002 工业自动化系统和继承.产品数据的表示和交换.第101部分:综合应用资源:绘图(技术勘误1)
// JIS B6016-1-1998 机创.润滑规程表示

