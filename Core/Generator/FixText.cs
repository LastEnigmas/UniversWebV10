using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Convertor
{
    public class FixText
    {
        public static string FixTexts(string text)
        {
            return text.Trim().ToLower();
        }
    }
}
