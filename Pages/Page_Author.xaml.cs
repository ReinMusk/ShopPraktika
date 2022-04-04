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

using Newtonsoft.Json;
using System.IO;

namespace ShopPraktika
{
    /// <summary>
    /// Interaction logic for Page_Author.xaml
    /// </summary>
    public partial class Page_Author : Page
    {

        //int count = 1;
        public static ObservableCollection<User> Users { get; set; }
        public Page_Author()
        {
            InitializeComponent();
            txt_login.Text = Properties.Settings.Default.Login;
        }

        private void Login_event(object sender, RoutedEventArgs e)
        {
            MainWindow.AuthUser = MainWindow.db.User.ToList().Find(c => c.Login == txt_login.Text.Trim() 
                                                                && c.Password == txt_password.Password.Trim());
            
            if (MainWindow.AuthUser == null)
            {
                MessageBox.Show("Пользователь не найден!");
                return;
            }

            if (chk_button.IsChecked.GetValueOrDefault())
            {
                Properties.Settings.Default.Login = MainWindow.AuthUser.Login;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.Login = null;
                Properties.Settings.Default.Save();
            }

            switch (MainWindow.AuthUser.RoleId)
            {
                case 1:
                    NavigationService.Navigate(new ProductsList(MainWindow.AuthUser));
                    break;
                case 3:
                    NavigationService.Navigate(new ProductsList(MainWindow.AuthUser));
                    break;
            }
        }

        private void Sign_event(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Register());
        }
    }
}
