
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Eind_P1.Core
{
    internal class SerClasses
    {

        public enum Conditie { Slecht, Matig, Goed, Nieuw }

        [System.Serializable]
        public class Checkup
        {
            public string Naam { get; set; }
            public string DatumBinnen { get; set; }

            public int Prijs { get; set; }

            public Conditie Conditie { get; set; }

            public bool Defect { get; set; }

        }

        [System.Serializable]
        public class Product
        {
            public Checkup[] checkups {  get; set; }
        }
    }
}
