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
        User usr;
        int actualPage;
        bool mounthbtn;
        int minCount;
         
        public ProductsList(User Newusr)
        {
            InitializeComponent();
            usr = Newusr;

            prod.ItemsSource = MainWindow.db.Product.ToList().Where(c => c.IsDeleted != true);

            var LvUnit = MainWindow.db.Unit.ToList();
            LvUnit.Insert(0, new Unit() { Id = -1, Name = "Все" });
            UnitCb.ItemsSource = LvUnit;
            UnitCb.DisplayMemberPath = "Name";

            max.Content = MainWindow.db.Product.ToList().Count;
            min.Content = max.Content;

            if (usr.RoleId != 3)
            {
                reports_btn.IsEnabled = true;
            }
        }

        private void Del_event(object sender, RoutedEventArgs e)
        {
            var isSelProduct = prod.SelectedItem as Product;
            if (isSelProduct != null)
            {
                if (usr.RoleId == 1)
                {
                    var result = MessageBox.Show("Удалить?", "", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        isSelProduct.IsDeleted = true;
                        MainWindow.db.Product.FirstOrDefault();
                        MainWindow.db.SaveChanges();
                        Refresh();
                    }
                }
                else
                    MessageBox.Show("Вы не админ");
               
            }
            else
                MessageBox.Show("Ничего не выбрано!");
        }

        private void Add_event(object sender, RoutedEventArgs e)
        {
            if (usr.RoleId == 1)
            {
                NavigationService.Navigate(new EditPage(new Product()));
            }
            else
                MessageBox.Show("Вы не админ");
        }

        private void EditBtnt_Click(object sender, RoutedEventArgs e)
        {
            var isSelProduct = prod.SelectedItem as Product;
            if (isSelProduct != null)
                NavigationService.Navigate(new EditPage(isSelProduct));
            else
                MessageBox.Show("Ничего не выбрано!");
        }

        private void Refresh()
        {
            var FilterProduct = (IEnumerable<Product>)MainWindow.db.Product.ToList().Where(c => c.IsDeleted != true);

            if (!string.IsNullOrWhiteSpace(SearchNameDescTb.Text))
                FilterProduct = FilterProduct.Where(c => c.Name.StartsWith(SearchNameDescTb.Text) || c.Description.StartsWith(SearchNameDescTb.Text));

            if (UnitCb.SelectedIndex > 0)
                FilterProduct = FilterProduct.Where(c => c.UnitId == (UnitCb.SelectedItem as Unit).Id || c.UnitId == -1);

            if (mounthbtn == true)
            {
                var date = DateTime.Now.Month;
                FilterProduct = FilterProduct.Where(c => c.AddDate.Month == date);

                mounthbtn = false;
            }

            if (DateCb.SelectedIndex == 1)
                FilterProduct = FilterProduct.OrderBy(c => c.AddDate);
            else if (DateCb.SelectedIndex == 2)
                FilterProduct = FilterProduct.OrderByDescending(c => c.AddDate);

            if (AlfCb.SelectedIndex == 1)
                FilterProduct = FilterProduct.OrderBy(c => c.Name);
            else if (AlfCb.SelectedIndex == 2)
                FilterProduct = FilterProduct.OrderByDescending(c => c.Name);


            if (SortCount.SelectedIndex > 0 && FilterProduct.Count() > 0)
            {
                var cbb = SortCount.SelectedItem as ComboBoxItem;
                int SelCount = Convert.ToInt32(cbb.Content);

                FilterProduct = FilterProduct.Skip(SelCount * actualPage).Take(SelCount);
                if (FilterProduct.Count() == 0)
                {
                    actualPage--;
                    return;
                }
                prod.ItemsSource = FilterProduct;
            }

            prod.ItemsSource = FilterProduct;

            minCount = FilterProduct.Count();

            min.Content = minCount.ToString();
        }

        private void UnitCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void SearchNameDescTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void DateMounthBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage = 0;
            mounthbtn = true;
            Refresh();
        }

        private void DateCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void AlfCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void SortCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void Reset_event(object sender, RoutedEventArgs e)
        {
            var cbb = SortCount.SelectedItem as ComboBoxItem;

            if (cbb == null || cbb.Content.ToString() == "Все")
            {
                prod.ItemsSource = MainWindow.db.Product.Where(c => c.IsDeleted != true).ToList();
                minCount = MainWindow.db.Product.ToList().Count;
            }
            else
            {
                int SelCount = Convert.ToInt32(cbb.Content);
                prod.ItemsSource = MainWindow.db.Product.ToList().Where(c => c.IsDeleted != true).Take(SelCount);
                minCount = SelCount;
            }
            
            min.Content = minCount;

            AlfCb.SelectedItem = null;
            DateCb.SelectedItem = null;
            UnitCb.SelectedItem = null;
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Refresh();
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            Refresh();
        }

        private void reports_btn_Click(object sender, RoutedEventArgs e)
        {
            if (usr.RoleId != 3)
                NavigationService.Navigate(new IntakesPage());
            else
                MessageBox.Show("Недостаточно прав");
        }

        private void orders_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderPage());
        }
    }
}
