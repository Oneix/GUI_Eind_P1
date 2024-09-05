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
            SetupMainWindow();

            AddDefaultProducts();
            RefreshProducts();
        }

        public static void SetupMainWindow()
        {
            MainWindow.Instance.MiddleRow.Visibility = Visibility.Collapsed;
            MainWindow.Instance.ButtomRow.Visibility = Visibility.Collapsed;

            MainWindow.Instance.ConditieComboBox.Items.Clear();
            foreach (var conditie in Enum.GetValues(typeof(SerClasses.Conditie)))
            {
                MainWindow.Instance.ConditieComboBox.Items.Add(conditie);
            }
            MainWindow.Instance.ConditieComboBox.SelectedIndex = 0;

            MainWindow.Instance.ShowView(MainWindow.Instance.CheckupsView);
        }


        public static void RefreshProducts()
        {
            ProductManager.RefreshProductsData();

            MainWindow.Instance.ParentProducts.Children.Clear();

            foreach(var prod in Data.Laptops)
            {
                ProductManager.AddProductGUI(prod);
            }
        }

        private static void AddDefaultProducts()
        {
            ProductManager.AddProductData(new SerClasses.Product(DateTime.Now.AddDays(5), "MSI AS11", "", 1799));
            ProductManager.AddProductData(new SerClasses.Product(DateTime.Now.AddDays(-3), "ASUS TUF", "", 1200));
        }


        public static void InspectProductGUI(SerClasses.Product prod)
        {
            if (prod != null)
            {
                MainWindow.Instance.MiddleRow.Visibility = Visibility.Visible;
                MainWindow.Instance.ButtomRow.Visibility = Visibility.Visible;

                MainWindow.Instance.NaamTextBox.Text = prod.Naam;
                MainWindow.Instance.PrijsTextBox.Text = prod.Prijs.ToString();

                if(prod?.Checkups?.Any() == true && MainWindow.Instance.ListBoxCheckUps.SelectedItem != null)
                {
                    MainWindow.Instance.ConditieHolder.Visibility = Visibility.Visible;
                    MainWindow.Instance.DefectHolder.Visibility = Visibility.Visible;

                    MainWindow.Instance.ConditieComboBox.SelectedValue = (int)prod.Checkups[MainWindow.Instance.ListBoxCheckUps.SelectedIndex].Conditie;
                    MainWindow.Instance.DefectComboBox.IsChecked = prod.Checkups[MainWindow.Instance.ListBoxCheckUps.SelectedIndex].Defect;
                }
                else
                {
                    MainWindow.Instance.ConditieHolder.Visibility = Visibility.Collapsed;
                    MainWindow.Instance.DefectHolder.Visibility = Visibility.Collapsed;
                }

                MainWindow.Instance.DatumPickerChecker.SelectedDate = prod.DatumBinnen;
                MainWindow.Instance.CalenderChecker.SelectedDate = prod.DatumBinnen;

                MainWindow.Instance.ListBoxCheckUps.Items.Clear();
                foreach (var check in prod.Checkups)
                {
                    MainWindow.Instance.ListBoxCheckUps.Items.Add(check);
                }

                if(string.IsNullOrEmpty(prod.PathImage))
                {
                    MainWindow.Instance.Image.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MainWindow.Instance.DisplaySelectedImage(prod.PathImage);
                }

            }
            else
            {
                MainWindow.Instance.MiddleRow.Visibility = Visibility.Collapsed;
                MainWindow.Instance.ButtomRow.Visibility = Visibility.Collapsed;
            }

            ProductManager.DisableFalseDirtyView();
        }
    }
}
