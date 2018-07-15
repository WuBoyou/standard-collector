using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    internal static class DataVerification
    {
        public static void CheckYear(int year)
        {
            if (year < 50 || year > 99 && year < 1950 || year > 2100)
                throw new ArgumentException("fullname");
        }
    }
}
