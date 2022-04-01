﻿using ShopPraktika.Data_;
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
    /// Interaction logic for Page_Author.xaml
    /// </summary>
    public partial class Page_Author : Page
    {

        int count = 1;
        public static ObservableCollection<User> Users { get; set; }
        public Page_Author()
        {
            InitializeComponent();
        }

        private void Login_event(object sender, RoutedEventArgs e)
        {
            User log = new User();

            try
            {
                Users = new ObservableCollection<User>(bd_connection.connection.User.ToList());
                log = Users.Where(a => a.Login.ToString() == txt_login.Text && a.Password.ToString() == txt_password.Password).FirstOrDefault();
            }
            catch (Exception)
            {

                throw new Exception("Ошибка");
            }

            if (log != null)
            {
                ProductsList window = new ProductsList(log);
                window.Show();
            }
            else
            {
                MessageBox.Show("Invalid user", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                ++count;
            }

            if (count % 4 == 0)
            {
                MessageBox.Show("Вы вводили неправильные данные больше 3 раз, блокировка 1 минута", "Блокировка");
                //заблокать кнопку на минуту
                count = 1;
            }
        }

        private void Sign_event(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page_Register());
        }

        private void chk_button_Checked(object sender, RoutedEventArgs e)
        {
            // чекбокс сделать
        }
    }
}
