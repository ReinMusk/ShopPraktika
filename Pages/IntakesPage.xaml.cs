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
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class IntakesPage : Page
    {
        public List<ProductIntake> ProductIntakes { get; set; }
        public IntakesPage()
        {
            InitializeComponent();
            
            ProductIntakes = MainWindow.db.ProductIntake.ToList();

            this.DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //NavigationService.Navigate(new IntakeProductsPage());
        }

        private void prodIntake_dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var intake = (sender as DataGrid).SelectedItem as ProductIntake;

            NavigationService.Navigate(new IntakesProductsPage(intake));
        }
    }
}
