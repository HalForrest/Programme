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
    /// MyUserControl2.xaml 的交互逻辑
    /// </summary>
    public partial class MyUserControl2 : UserControl
    {
        public MyUserControl2()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
            string[] ProductRowName = new string[50];
            string str_sql = "Select name from syscolumns Where id=object_id('Product')";//获得表的列名
            Helper.DBManager.DBConnect();
            Helper.DBManager.DataHandle(str_sql,Helper.func.select);
            int i = 0;
            if(Helper.DBManager.dt==null)
            {
                MessageBox.Show("数据库错误！");
            }
            foreach( DataRow dr in Helper.DBManager.dt.Rows)
            {
                ProductRowName[i] = dr[0].ToString();
                i++;
            }
            SelectcomboBox.ItemsSource = ProductRowName;
            SelectcomboBox.SelectedIndex = 0;
        }

        private void SelectBtn_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Selectbtn_Click(object sender, RoutedEventArgs e)
        {
            string selectstr = SelecttextBox.Text.ToString();
            string rowName = SelectcomboBox.SelectedValue.ToString();
            string str_sql = "select * from Product where "+rowName+"="+selectstr;
            Helper.DBManager.DBConnect();
            Helper.DBManager.DataHandle(str_sql, Helper.func.select);
            dataGrid.ItemsSource = Helper.DBManager.dt.DefaultView;
        }
    }
}
