
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
            public string Datum;

            public Conditie Conditie { get; set; }

            public bool Defect { get; set; }

        }

        [System.Serializable]
        public class Product
        {
            public string Naam { get; set; }
            public string PathImage { get; set; }

            public int Prijs { get; set; }

            public Checkup[] Checkups {  get; set; }

            public Product(string naam = "", string pathImage = "", int prijs = 0, Checkup[] checkups = null)
            {
                Naam = naam;
                PathImage = pathImage;
                Prijs = prijs;

                if (checkups != null)
                {
                    Checkups = checkups;
                }
            }
        }
    }
}
