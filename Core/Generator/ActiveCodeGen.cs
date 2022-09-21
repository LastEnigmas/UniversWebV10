using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Generator
{
    public class ActiveCodeGen
    {
        public static string GenerateCode()
        {
            return Guid.NewGuid().ToString().Replace('-', '2');
        }
    }
}
