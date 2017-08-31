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
using System.Windows.Shapes;
using CommonLib;

namespace Programme_V170824
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : BaseWindow
    {
        public Login()
        {
            InitializeComponent();
        }
        #region 没用事件
        /*
                private void close_tips_MouseEnter(object sender, MouseEventArgs e)
                {

                }

                private void close_tips_MouseLeave(object sender, MouseEventArgs e)
                {

                }
                */
        #endregion
        private void btn_Login_Click(object sender, RoutedEventArgs e)
        {
            string name =  LoginName.Text.ToString();
            string pwd = LoginPassword.Password.ToString();
            Helper.DBManager.DBConnect();
            string str1 = "Select * from MyUser where user_Name='"+name+"'";
            Helper.DBManager.DataHandle(str1, Helper.func.select);
            string pw = (string)Helper.DBManager.dt.Rows[0][1];
            pw= pw.Trim();
            if (Helper.DBManager.dt!=null)
            {
                if(pw==pwd)
                {
                    this.Close();
                    ProductWindow pd = new ProductWindow();
                    pd.Show();
                }
                else
                {

                    MessageBox.Show(pwd+"密码错误" + pw, "Error", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("账号不存在", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Login_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btn_Login_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
