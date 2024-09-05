using GUI_Eind_P1.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringManager
{
    class StringManagement
    {
        public static string GetUniqueName(string baseName)
        {
            int duplicateCount = Data.Laptops.Count(l => l.Naam.StartsWith(baseName));
            return $"{baseName}_{duplicateCount + 1}";
        }
    }
}
