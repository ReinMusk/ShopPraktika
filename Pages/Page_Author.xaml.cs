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
        public static ObservableCollection<User> Users { get; set; }
        public Page_Author()
        {
            InitializeComponent();
            txt_login.Text = Properties.Settings.Default.Login;

            if (Properties.Settings.Default.LogTime < DateTime.Now && 
                Properties.Settings.Default.LogCount >= 3)
            {
                Properties.Settings.Default.LogCount = 1;
                Properties.Settings.Default.Save();
            }
        }

        private void Login_event(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.LogTime < DateTime.Now)
            {
                MainWindow.AuthUser = MainWindow.db.User.ToList().Find(c => c.Login == txt_login.Text.Trim()
                                                                && c.Password == txt_password.Password.Trim());

                if (MainWindow.AuthUser == null)
                {
                    if (Properties.Settings.Default.LogCount < 3)
                    {
                        Properties.Settings.Default.LogCount++;
                        Properties.Settings.Default.Save();
                        MessageBox.Show("Неверный логин или пароль");
                    }
                    else
                    {
                        MessageBox.Show("Слишком много неверных попыток ввода: блокировка 1 минута");
                        Properties.Settings.Default.LogTime = DateTime.Now.AddMinutes(1);
                        Properties.Settings.Default.Save();
                    }
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
                        Properties.Settings.Default.LogCount = 1;
                        Properties.Settings.Default.Save();
                        NavigationService.Navigate(new ProductsList(MainWindow.AuthUser));
                        break;
                    case 3:
                        Properties.Settings.Default.LogCount = 1;
                        Properties.Settings.Default.Save();
                        NavigationService.Navigate(new ProductsList(MainWindow.AuthUser));
                        break;
                }
            }
            else
            {
                MessageBox.Show($"Доступ заблокирован. Повторите попытку в {Properties.Settings.Default.LogTime}");
            }
        }

        private void Sign_event(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Register());
        }
    }
}
