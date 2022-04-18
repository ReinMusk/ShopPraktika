using ShopPraktika.Data_;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for Page_Register.xaml
    /// </summary>
    public partial class Page_Register : Page
    {
        int gendr;
        public static ObservableCollection<User> Users { get; set; }
        public Page_Register()
        {
            InitializeComponent();
        }

        private void Save_event(object sender, RoutedEventArgs e)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*])(?=.*[^a-zA-Z0-9])\S{6,16}$");
            
            if (cb_gender.Text == "Мужской")
            {
                gendr = 1;
            }
            else if (cb_gender.Text == "Женский")
            {
                gendr = 2;
            
            }
            bool isPass = regex.IsMatch(txt_password.Password);
            if (isPass)
            {
                try
                {
                    User a = new User
                    {
                        Login = txt_login.Text,
                        Password = txt_password.Password,
                        RoleId = 3
                    };
                    Client b = new Client
                    {
                        FIO = tb_FIO.Text,
                        Email = tb_email.Text,
                        NumberPhone = tb_phone.Text,
                        AddDate = DateTime.Now,
                    };
                    b.GenderId = gendr;
                    bd_connection.connection.User.Add(a);
                    bd_connection.connection.SaveChanges();
                    MessageBox.Show("OK");
                    NavigationService.GoBack();
                }
                catch (Exception)
                {
                    throw new Exception("Error");
                }
            }
            else
            {
                MessageBox.Show("Придумай пароль получше");
            }

        }

        private void Cancel_event(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void tb_phone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}