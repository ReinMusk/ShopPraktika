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
        Product product;

        public EditPage(Product newProd)
        {
            InitializeComponent();

            CountryCb.ItemsSource = MainWindow.db.Country.ToList().Where(x => x.IsDeleted == false);
            CountryCb.DisplayMemberPath = "Name";

            UnitCb.ItemsSource = MainWindow.db.Unit.ToList();

            product = newProd;
            this.DataContext = product;
            if (product.Id != 0)
            {
                spCountry.Visibility = Visibility.Visible;
                CountryLabel.Visibility = Visibility.Visible;
            }
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountryCb.SelectedIndex >= 0)
            {
                var ProdCountry = new ProductCountry();
                var sel = CountryCb.SelectedItem as Country;
                ProdCountry.ProductId = product.Id;
                ProdCountry.CountryId = sel.Id;
                var isCountry = MainWindow.db.ProductCountry.Where(c => c.CountryId == sel.Id && c.ProductId == product.Id).Count();
                if (isCountry == 0)
                {
                    MainWindow.db.ProductCountry.Add(ProdCountry);
                    MainWindow.db.SaveChanges();
                    UpdateCountryList();
                }
            }
        }

        private void DelCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CountryLv.SelectedItem != null)
            {
                var selProductCountry = MainWindow.db.ProductCountry.ToList().Find(c => c.ProductId == product.Id && c.CountryId == (CountryLv.SelectedItem as ProductCountry).CountryId);
                MainWindow.db.ProductCountry.Remove(selProductCountry);
                MainWindow.db.SaveChanges();
                UpdateCountryList();
            }
        }
        private void UpdateCountryList()
        {
            CountryLv.ItemsSource = MainWindow.db.ProductCountry.Where(e => e.ProductId == product.Id).ToList();
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
            if (product.Id != 0)
            {
                bd_connection.connection.Product.FirstOrDefault<Product>();
                MainWindow.db.SaveChanges();
                NavigationService.GoBack();
            }
            else
            {
                MainWindow.db.Product.Add(product);
                MainWindow.db.SaveChanges();
                NavigationService.GoBack();
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta);
            e.Handled = true;
        }
    }
}
