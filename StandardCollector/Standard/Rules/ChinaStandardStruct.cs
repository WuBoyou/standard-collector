using System;

namespace Standard.Rules
{
    public class ChinaStandardStruct : StandardStruct
    {
        /// <summary>标准前缀</summary>
        private string prefix;
        /// <summary>标准类型标记</summary>
        private string type;
        /// <summary>标准代号</summary>
        private string code;
        /// <summary>发布年份</summary>
        private int year;
        /// <summary>标准名称</summary>
        private string name;

        public ChinaStandardStruct(string prefix, string type, string code, int year, string name)
        {
            this.prefix = prefix;
            this.type = type;
            this.code = code;
            this.year = year;
            this.name = name;
        }

        /// <summary>标准前缀。</summary>
        /// <remarks>表示标准分类，如国家标准（GB）、行业标准、地方标准（DB）。</remarks>
        public string Prefix
        {
            get { return this.prefix; }
        }

        /// <summary>标准类型标记。</summary>
        /// <remarks>如为强制标准，则为空；如为推荐性标准，则为“/T”；如为指导性标准，则为“/Z”。</remarks>
        public string Type
        {
            get { return this.type; }
        }

        /// <summary>标准代号。</summary>
        public string StandardCode
        {
            get { return this.code; }
        }

        /// <summary>标准发布年份。</summary>
        public int Year
        {
            get { return this.year; }
        }

        protected override string GetStandardNumber()
        {
            if (string.IsNullOrEmpty(prefix) || string.IsNullOrEmpty(code))
                return string.Empty;

            return string.Format("{0}{1} {2}-{3}", this.prefix, this.type, this.code, this.year);
        }

        protected override string GetStandardName()
        {
            return this.name;
        }

        public override string ToString()
        {
            return this.GetFullName();
        }
    }
}
