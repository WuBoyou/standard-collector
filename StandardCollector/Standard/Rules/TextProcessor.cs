using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standard.Rules
{
    internal static class TextProcessor
    {
        public static string ReplaceUnderlineCharBySpaceCharacter(string text)
        {
            return text.Replace('_', ' ');
        }
    }
}
