using GUI_Eind_P1.Core;
using GUI_Eind_P1;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using static GUI_Eind_P1.Core.SerClasses;
using StringManager;

namespace ProductManagement
{
    class ProductManager
    {
        public static int CurrentLaptop = -1;

        public static bool IsInspectingProduct = false;
        public static SerClasses.Product CurrentProductInspected = null;

        public async static void DisableFalseDirtyView()
        {
            await Task.Delay(50);
            Data.DirtyView = false;
        }

        public static void InspectProduct(SerClasses.Product product)
        {
            if (IsInspectingProduct && CurrentLaptop != product.Index && Data.DirtyView == true)
            {
                Data.DirtyView = false;
                switch (MessageBox.Show("Wil je het product opslaan?", "Product Manager", MessageBoxButton.YesNoCancel))
                {
                    case MessageBoxResult.Yes:
                        SaveProduct();
                        break;
                    case MessageBoxResult.Cancel:
                        return;
                }
            }

            CurrentLaptop = product.Index;

            IsInspectingProduct = true;
            CurrentProductInspected = product;
            UiElementsManagement.InspectProductGUI(product);
        }
        public static void StopInspecting()
        {
            Data.DirtyView = false;

            CurrentLaptop = -1;

            IsInspectingProduct = false;
            CurrentProductInspected = null;
            UiElementsManagement.InspectProductGUI(null);
        }

        public static void RefreshProductsData()
        {
            int i = 0;
            foreach (var prod in Data.Laptops)
            {
                prod.Index = i;
                i++;
            }
        }

        public static void AddProductData(SerClasses.Product product)
        {
            Data.Laptops.Add(product);
        }

        public static void AddProductGUI(SerClasses.Product product)
        {
            Grid mainGrid = new Grid
            {
                Width = 226,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            Button productButton = new Button
            {
                Margin = new Thickness(5),
                Background = (Brush)Application.Current.Resources["BorderBrush"],
                Style = (Style)Application.Current.Resources["DefaultButton"]
            };
            productButton.Click += (sender, e) => MainWindow.Instance.ProductClick(sender, e);
            productButton.Tag = product;

            Grid innerGrid = new Grid
            {
                IsHitTestVisible = false
            };

            PackIconMaterial icon = new PackIconMaterial
            {
                Kind = PackIconMaterialKind.Laptop,
                Foreground = (Brush)Application.Current.Resources["TextBrush"],
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(12, 0, 0, 0),
                Height = 50,
                Width = 50
            };

            StackPanel textPanel = new StackPanel
            {
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(70, 0, 0, 0)
            };

            TextBlock titleText = new TextBlock
            {
                Style = (Style)Application.Current.Resources["TitleText"],
                Text = product.Naam,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            TextBlock descriptionText = new TextBlock
            {
                Style = (Style)Application.Current.Resources["MiniText"],
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            if (product?.Checkups?.Any() == true)
            {
                var lastCheckup = product.Checkups.Last();
                string defectStatus = lastCheckup.Defect ? "Defect" : "Werkend";

                descriptionText.Text = $"● {lastCheckup.Conditie} ● {defectStatus} ● € {product.Prijs:N2}";
            }
            else
            {
                descriptionText.Text = $"● Check-up nodig ● € {product.Prijs:N2}";
            }

            textPanel.Children.Add(titleText);
            textPanel.Children.Add(descriptionText);

            innerGrid.Children.Add(icon);
            innerGrid.Children.Add(textPanel);

            mainGrid.Children.Add(productButton);
            mainGrid.Children.Add(innerGrid);


            MainWindow.Instance.ParentProducts.Children.Add(mainGrid);
        }

        public static void SaveProduct()
        {
            Data.Laptops[CurrentLaptop].Naam = MainWindow.Instance.NaamTextBox.Text;
            Data.Laptops[CurrentLaptop].Prijs = int.Parse(MainWindow.Instance.PrijsTextBox.Text);

            Data.Laptops[CurrentLaptop].Checkups.Clear();
            foreach (object prod in MainWindow.Instance.ListBoxCheckUps.Items)
            {
                SerClasses.Checkup product = prod as SerClasses.Checkup;
                if (product != null)
                {
                    Data.Laptops[CurrentLaptop].Checkups.Add(product);
                }
            }


            Data.Laptops[CurrentLaptop].DatumBinnen = MainWindow.Instance.DatumPickerChecker.SelectedDate;

            UiElementsManagement.RefreshProducts();

            Data.DirtyView = false;
        }

        public static void RemoveProduct()
        {
            Data.Laptops.RemoveAt(CurrentLaptop);
            UiElementsManagement.RefreshProducts();

            StopInspecting();
        }

        public static void Dublicate()
        {
            Product originalLaptop = Data.Laptops[CurrentLaptop];
            List<SerClasses.Checkup> checkups = new List<SerClasses.Checkup>();
            foreach (Checkup checkup in originalLaptop.Checkups)
            {
                checkups.Add(checkup);
            }
            Product newlaptop = new Product(originalLaptop.DatumBinnen.Value, StringManagement.GetUniqueName(originalLaptop.Naam), originalLaptop.PathImage, originalLaptop.Prijs, checkups);
            Data.Laptops.Add(newlaptop);

            UiElementsManagement.RefreshProducts();
        }

    }
}
