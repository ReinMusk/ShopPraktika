using ShopPraktika.Data_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ShopPraktika
{
    /// <summary>
    /// Interaction logic for EditPage.xaml
    /// </summary>
    public partial class EditPage : Page
    {
        public static ObservableCollection<Product> products { get; set; }
        public static ObservableCollection<Unit> unit { get; set; }
        public static ObservableCollection<Country> country { get; set; }

        Product product;
        public EditPage(Product newProd)
        {
            InitializeComponent();

            product = newProd;

            unit = new ObservableCollection<Unit>(bd_connection.connection.Unit.ToList());
            country = new ObservableCollection<Country>(bd_connection.connection.Country.ToList());


            this.DataContext = this;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            product.UnitId = (cb_unit.SelectedItem as Unit).Id;
            product.AddDate = DateTime.Now;

            //AddCountryBtn.Visibility = Visibility.Visible;
            //DelCountryBtn.Visibility = Visibility.Visible;
        }

        private void btn_newphoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg"
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                product.Photo = File.ReadAllBytes(openFile.FileName);
                ForPhoto.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            bd_connection.connection.SaveChanges();
            NavigationService.GoBack();
        }
    }
}
