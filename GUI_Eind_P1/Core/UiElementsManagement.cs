using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ProductManagement;
namespace GUI_Eind_P1.Core
{
    class UiElementsManagement
    {
        public static void Start()
        {
            ProductManager.AddProductData(new SerClasses.Product("test", "", 2123));
            RefreshProducts();
        }

        public static void RefreshProducts()
        {
            MainWindow.Instance.ParentProducts.Children.Clear();

            foreach(var prod in Data.Laptops)
            {
                ProductManager.AddProductGUI(prod);
            }
        }

    }
}
