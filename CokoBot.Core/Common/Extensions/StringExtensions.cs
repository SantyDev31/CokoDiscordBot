using CokoBot.App.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Extension
{
    public static class StringExtensions
    {
        public static IEnumerable<int> AllIndicesOf(this string text, string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentNullException(nameof(pattern));
            }
            return AllIndexOfUtil.Kmp(text, pattern);
        }

        public static string ReplaceAt(this string text, int index, char newChar)
        {
            if (text == null)
            {
                throw new ArgumentNullException("input");
            }

            if (index < 0 || index > text.Length - 1)
            {
                throw new ArgumentNullException("index");
            }

            var chars = text.ToCharArray();
            chars[index] = newChar;
            return new string(chars);
        }
    }
}
