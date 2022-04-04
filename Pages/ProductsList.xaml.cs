using ShopPraktika.Data_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace ShopPraktika
{
    /// <summary>
    /// Interaction logic for ProductsList.xaml
    /// </summary>
    public partial class ProductsList : Page
    {
        public static ObservableCollection<Product> products { get; set; }
        public static ObservableCollection<Unit> unit { get; set; }
        public User user;
        public ProductsList(User usr)
        {
            InitializeComponent();
            products = new ObservableCollection<Product>(bd_connection.connection.Product.ToList());
            unit = new ObservableCollection<Unit>(bd_connection.connection.Unit.ToList());
            var Prod = new Product();

            user = usr;

            unit.Add(new Unit { Name = "Все" });

            this.DataContext = this;
        }

        private void tb_Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Poisk.Text != "")
            {
                prod.SelectedItem = null;
                prod.ItemsSource = new ObservableCollection<Product>(bd_connection.connection.Product.Where(z => (z.Name.Contains(tb_Poisk.Text) || z.Description.Contains(tb_Poisk.Text))).ToList());
            }
        }

        private void prod_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = (sender as ListView).SelectedItem as Product;

            NavigationService.Navigate(new EditPage(product));
        }

        private void Reset_event(object sender, RoutedEventArgs e)
        {
            prod.ItemsSource = products.OrderBy(x => x.Id).ToList();

            Unit_comb.SelectedItem = null;
        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as Unit;

            if (a.Name == "Все")
            {
                prod.ItemsSource = products.OrderBy(x => x.UnitId).ToList();
            }
            else
                prod.ItemsSource = products.Where(x => x.UnitId == a.Id).ToList();
        }


        private void btn_Alfabet_Click(object sender, RoutedEventArgs e)
        {
            prod.ItemsSource = products.OrderBy(x => x.Name).ToList();
        }

        private void btn_InMounth_Click(object sender, RoutedEventArgs e)
        {
            prod.ItemsSource = products.Where(x => x.AddDate.Month == DateTime.Now.Month).ToList();
        }

    }
}
