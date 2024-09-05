using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GUI_Eind_P1.Core
{
    class UiElementsManagement
    {
        public static void Start()
        {
            AddProductData(new SerClasses.Product("test", "", 2123));
            RefreshProducts();
        }

        public static void RefreshProducts()
        {
            MainWindow.Instance.ParentProducts.Children.Clear();

            foreach(var prod in Data.Laptops)
            {
                AddProductGUI(prod);
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
                descriptionText.Text = $"● Moet gecheckd worden";
            }

            textPanel.Children.Add(titleText);
            textPanel.Children.Add(descriptionText);

            innerGrid.Children.Add(icon);
            innerGrid.Children.Add(textPanel);

            mainGrid.Children.Add(productButton);
            mainGrid.Children.Add(innerGrid);

            
            MainWindow.Instance.ParentProducts.Children.Add(mainGrid);
        }

    }
}
