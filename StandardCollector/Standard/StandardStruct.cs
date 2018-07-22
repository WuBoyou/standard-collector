using System;

namespace Standard
{
    public abstract class StandardStruct
    {

        protected StandardStruct()
        {
        }

        /// <summary>获取标准名称。</summary>
        public string StandardName
        {
            get { return this.GetStandardName(); }
        }

        /// <summary>获取标准编号</summary>
        public string StandardNumber
        {
            get { return this.GetStandardNumber(); }
        }

        protected abstract string GetStandardNumber();

        protected abstract string GetStandardName();

        /// <summary>返回标准的完全名称（标准编号+标准名称）。</summary>
        /// <returns></returns>
        public virtual string GetFullName()
        {
            if (string.IsNullOrEmpty(this.GetStandardName()))
            {
                return this.GetStandardNumber();
            }

            return string.Format("{0} {1}", this.GetStandardNumber(), this.GetStandardName());
        }

        public override string ToString()
        {
            return this.GetFullName();
        }
    }
}
