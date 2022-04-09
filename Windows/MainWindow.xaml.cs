using System;
using System.Collections.Generic;
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

using ShopPraktika.Data_;

namespace ShopPraktika
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ShopEntities1 db = new ShopEntities1();
        public static User AuthUser;
        public MainWindow()
        {
            InitializeComponent();


            Log_frame.NavigationService.Navigate(new Page_Author());
        }


    }
}
