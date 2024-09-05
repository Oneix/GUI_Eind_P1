
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI_Eind_P1.Core
{
    internal class SerClasses
    {

        public enum Conditie {Kiezen, Slecht, Matig, Goed, Nieuw }

        [System.Serializable]
        public class Checkup
        {
            public Conditie Conditie { get; set; }

            public bool Defect { get; set; }

        }

        [System.Serializable]
        public class Product
        {
            public int Index;

            public string Naam { get; set; }
            public string PathImage { get; set; }
            public DateTime? DatumBinnen;

            public int Prijs { get; set; }

            public List<Checkup> Checkups = new List<Checkup>();

            public Product(DateTime datumBinnen, string naam = "", string pathImage = "", int prijs = 0, List<Checkup> checkups = null)
            {
                Naam = naam;
                PathImage = pathImage;
                Prijs = prijs;

                if (checkups != null)
                {
                    Checkups = checkups;
                }

                DatumBinnen = datumBinnen;
            }
        }
    }
}
