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
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Programme_V170824
{
    /// <summary>
    /// Login.xaml 的交互逻辑
    /// </summary>
    public partial class Login : BaseWindow
    {
        public DataTable dt;
        public Login()
        {
            InitializeComponent();
            this.Closing += new System.ComponentModel.CancelEventHandler(BaseWindow_Closing);
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
            dt = Helper.DBManager.DataHandle(str1, Helper.func.select) as DataTable;
            string pw = (string)dt.Rows[0][1];
            pw= pw.Trim();
            if (dt!=null)
            {
                if(pw==pwd)
                {
                    
                    this.Close();
                    
                    
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

        private void BaseWindow_Closed(object sender, EventArgs e)
        {
            ProductWindow pd = new ProductWindow();
            pd.Show();
        }
    }
}
