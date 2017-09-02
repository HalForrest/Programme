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
using System.Data;
using System.Data.SqlClient;

namespace Programme_V170824
{
    /// <summary>
    /// MyUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyUserControl : UserControl
    {
        public string str;
        public MyUserControl(string kind)
        {
            InitializeComponent();
            str = kind;
            if(str == "1")
            {
                textBlock1.Text += "查询\n";
            }
            if(str == "2")
            {
                textBlock1.Text += "添加\n";
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string str_sql = "Select * from Product";
            
            
            Helper.DBManager.DBConnect();//测试用

            DataTable dt =  Helper.DBManager.DataHandle(str_sql, Helper.func.select);
            if(dt!=null)
            {
                textBlock1.Text += "数据库操作成功！\n";
                dataGrid.ItemsSource = dt.DefaultView;
            }
            Helper.DBManager.DBConnectionClose();
        }
    }
}
