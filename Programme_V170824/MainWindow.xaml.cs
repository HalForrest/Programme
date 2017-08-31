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

namespace Programme_V170824
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// 
    /// 登陆界面
    /// 根据不同的账号
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Visibility = Visibility.Hidden;
        }

        private void button_Login_Click(object sender, RoutedEventArgs e)
        {
            if ((string)button_Login.Content == "登陆")
            {
                button_Login.Content = "取消";
            }
            else if((string)button_Login.Content == "取消")
            {
                button_Login.Content = "登陆";
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Login _login = new Login();
            _login.Show();
        }
    }
}
