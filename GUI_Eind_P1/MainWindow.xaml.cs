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
            ProductManager.Dublicate();
        }
        private void AddPic_Click(object sender, RoutedEventArgs e)
        {

        }
        public void ProductClick(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            SerClasses.Product product = button.Tag as SerClasses.Product;

            ListCheckUps.UnselectAll();
            ProductManager.InspectProduct(product);
        }


        private void ConditieComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }



        private void DefectComboBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void DefectComboBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }




        private void PrijsTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NaamTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


         
        private void CalenderChecker_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (canChangeDates)
            {
                DelayDateChanger();
                DatumPickerChecker.SelectedDate = CalenderChecker.SelectedDate;
            }
        }

        private void DatumPickerChecker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
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
    }
}