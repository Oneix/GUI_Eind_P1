using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GUI_Eind_P1.Core;
using Microsoft.Win32;
using ProductManagement;

namespace GUI_Eind_P1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //static vars

        //intance
        public static MainWindow Instance;





        //private vars

        //booleans
       bool canChangeDates = true;




        public MainWindow()
        {
            InitializeComponent();
            SetupVars();
            UiElementsManagement.Start();
        }


        private void SetupVars()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                if(Instance != this)
                {
                    Instance.Close();
                    Instance = this;
                }
            }

            canChangeDates = true;
        }

        private void Opslaan_Click(object sender, RoutedEventArgs e)
        {
            ProductManager.SaveProduct();
        }

        private void Verwijderen_Click(object sender, RoutedEventArgs e)
        {
            ProductManager.RemoveProduct();
        }

        private void Dupliceren_Click(object sender, RoutedEventArgs e)
        {
            ProductManager.SaveProduct();
            ProductManager.Dublicate();
        }
        private void AddPic_Click(object sender, RoutedEventArgs e)
        {
            string fullPath_ = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string saveDirectory = System.IO.Path.Combine(appDataFolder, "EasyAdmin", "Images");
                Directory.CreateDirectory(saveDirectory);

                string imagePath = openFileDialog.FileName;
                string fileName = $"uploaded_image_{DateTime.Now:yyyyMMddHHmmss}.jpg";
                string uploadedImagePath = System.IO.Path.Combine(saveDirectory, fileName);

                File.Copy(imagePath, uploadedImagePath, true);

                Data.Laptops[ProductManager.CurrentLaptop].PathImage = uploadedImagePath;

                DisplaySelectedImage(imagePath);
            }
        }
        public void DisplaySelectedImage(string imagePath)
        {
            try
            {
                Image.Visibility = Visibility.Visible;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();
                ImageBrush.ImageSource = bitmap;
            }
            catch
            {
                MessageBox.Show($"foutje", "Product Manager", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ProductClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SerClasses.Product product = button.Tag as SerClasses.Product;

            ListBoxCheckUps.UnselectAll();
            ProductManager.InspectProduct(product);
        }


        private void ConditieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.DirtyView = true;
            if (ListBoxCheckUps.SelectedItem != null && ListBoxCheckUps.SelectedIndex != -1)
            {
                SerClasses.Checkup checkup = ListBoxCheckUps.SelectedItem as SerClasses.Checkup;
                checkup.Conditie = (SerClasses.Conditie)ConditieComboBox.SelectedIndex;
            }
        }

        private void DefectComboBox_Checked(object sender, RoutedEventArgs e)
        {
            Data.DirtyView = true;

            if (ListBoxCheckUps.SelectedItem != null && ListBoxCheckUps.SelectedIndex != -1)
            {
                SerClasses.Checkup checkup = ListBoxCheckUps.SelectedItem as SerClasses.Checkup;
                checkup.Defect = (bool)DefectComboBox.IsChecked;
            }
        }

        private void DefectComboBox_Unchecked(object sender, RoutedEventArgs e)
        {
            Data.DirtyView = true;

            if (ListBoxCheckUps.SelectedItem != null && ListBoxCheckUps.SelectedIndex != -1)
            {
                SerClasses.Checkup checkup = ListBoxCheckUps.SelectedItem as SerClasses.Checkup;
                checkup.Defect = (bool)DefectComboBox.IsChecked;
            }
        }




        private void PrijsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Data.DirtyView = true;
        }

        private void NaamTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Data.DirtyView = true;
        }



        private void CalenderChecker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.DirtyView = true;
            if (canChangeDates)
            {
                DelayDateChanger();
                DatumPickerChecker.SelectedDate = CalenderChecker.SelectedDate;
            }
        }

        private void DatumPickerChecker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Data.DirtyView = true;
            if (canChangeDates)
            {
                DelayDateChanger();
                CalenderChecker.SelectedDate = DatumPickerChecker.SelectedDate;
            }
        }

        public async void DelayDateChanger()
        {
            canChangeDates = false;
            await Task.Delay(100);
            canChangeDates = true;
        }

        private void NumberOnlyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !isnumber(e.Text);
        }

        private bool isnumber(string text)
        {
            return int.TryParse(text, out _);
        }

        private void VerwijderListboxItem_Click(object sender, RoutedEventArgs e)
        {
            Data.DirtyView = true;
            if (ListBoxCheckUps.SelectedItem != null && ListBoxCheckUps.SelectedIndex != -1)
            {
                ListBoxCheckUps.Items.RemoveAt(ListBoxCheckUps.SelectedIndex);
            }
            else
            {
                MessageBox.Show("Selecteer eerst een item.", "Product Manager", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ToevoegenListboxItem_Click(object sender, RoutedEventArgs e)
        {
            Data.DirtyView = true;
            SerClasses.Checkup checkup = new SerClasses.Checkup();
            checkup.checkupNaam = $"Check-up {ListBoxCheckUps.Items.Count}";
            ListBoxCheckUps.Items.Add(checkup);
        }


        

        private void ListBoxCheckUps_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBoxCheckUps.SelectedItem != null && ListBoxCheckUps.SelectedIndex != -1)
            {
                SerClasses.Checkup check = (SerClasses.Checkup)ListBoxCheckUps.SelectedItem;

                ConditieHolder.Visibility = Visibility.Visible;
                DefectHolder.Visibility = Visibility.Visible;

                ConditieComboBox.SelectedIndex = (int)check.Conditie;
                DefectComboBox.IsChecked = (bool)check.Defect;
            }
            else
            {
                ConditieHolder.Visibility = Visibility.Collapsed;
                DefectHolder.Visibility = Visibility.Collapsed;
            }
        }

        private void Sluiten(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}