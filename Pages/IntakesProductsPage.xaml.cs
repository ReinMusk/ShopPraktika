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
    /// Interaction logic for IntakesProductsPage.xaml
    /// </summary>
    public partial class IntakesProductsPage : Page
    {
        public static ObservableCollection<ProductIntakeProduct> intakeProducts { get; set; }
        public static ObservableCollection<ProductIntakeProduct> sum { get; set; }
        public IntakesProductsPage(ProductIntake intake)
        {
            InitializeComponent();

            intakeProducts = new ObservableCollection<ProductIntakeProduct>((MainWindow.db.ProductIntakeProduct.Where(n => n.ProductIntake.Id == intake.Id).ToList()));
            intakeProducts.Sum(n => n. n.Count + n.PriceUnit);

            cb_supplier.SelectedItem = intake.Supplier.Name;

            cb_supplier.ItemsSource = bd_connection.connection.Supplier.ToList();


            this.DataContext = this;
        }

        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btn_add_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
