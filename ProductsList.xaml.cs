using ShopPraktika.Data;
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
using System.Windows.Shapes;

namespace ShopPraktika
{
    /// <summary>
    /// Interaction logic for ProductsList.xaml
    /// </summary>
    public partial class ProductsList : Window
    {
        public static ObservableCollection<Product> products { get; set; }
        public User user;
        public ProductsList(User usr)
        {
            InitializeComponent();
            products = new ObservableCollection<Product>(bd_connection.connection.Product.ToList());
            var Prod = new Product();

            user = usr;

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

        }


        private void Del_event(object sender, RoutedEventArgs e)
        {
            //тут продукт взять

            if (user.Id != 3)
            {
                if (MessageBox.Show("If the file save fails, do you want to automatically try again?",
                    "Save file",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // удалить bd_connection.connection.Product.Remove
                }
            }
            else
            {
                MessageBox.Show("Нет прав", "Ошибка");
            }
        }

        private void Reset_event(object sender, RoutedEventArgs e)
        {

        }

        private void Unit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (sender as ComboBox).SelectedItem as Unit;

            prod.ItemsSource = products.Select(x => x.Unit.Id == a.Id).ToList();
        }


        int count = 0;
        private void btn_Alfabet_Click(object sender, RoutedEventArgs e)
        {

            if (count % 2 == 0)
            {
                prod.ItemsSource = products.OrderBy(x => x.Name).ToList();
                var temp = products.OrderBy(x => x.Name).ToList();
                temp.Reverse();
                prod.ItemsSource = temp;
            }
            else
            {
                prod.ItemsSource = products.OrderBy(x => x.Name).ToList();
            }
        }

        private void btn_InMounth_Click(object sender, RoutedEventArgs e)
        {
            prod.ItemsSource = products.Select(x => x.AddDate.Month == DateTime.Now.Month).ToList();
        }
    }
}
