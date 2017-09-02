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
        public DataTable dt;
        public MyUserControl2()
        {
            InitializeComponent();
            init();
        }

        public void init()
        {
           // textBox.Text += "数据库链接中。。。";
            string[] ProductRowName = new string[50];
            string str_sql = "Select name from syscolumns Where id=object_id('Product')";//获得表的列名
            Helper.DBManager.DBConnect();
            dt = Helper.DBManager.DataHandle(str_sql,Helper.func.select);
            int i = 0;
            if(dt==null)
            {
                MessageBox.Show("数据库错误！");
            }
            textBox.Text += "数据库连接成功！\n";
            foreach( DataRow dr in dt.Rows)
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
            string str_sql = "select * from Product where "+rowName+" like '%"+selectstr+"%'";
            Helper.DBManager.DBConnect();
            Helper.DBManager.DataHandle(str_sql, Helper.func.select);
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            int a = dataGrid.SelectedIndex;
            if (a  < dt.Rows.Count)
            {

                textBox.Text += "选中行号为" + dataGrid.SelectedIndex.ToString() + "\n";
                textBox.Text += "值为：" + dt.Rows[a][0].ToString();

                textBox_ProductNum.Text = dt.Rows[a][0].ToString();
                textBox_SupplierNum.Text = dt.Rows[a][1].ToString();
                textBox_Name.Text = dt.Rows[a][2].ToString();
                textBox_Para.Text = dt.Rows[a][3].ToString();
                textBox_Address.Text = dt.Rows[a][4].ToString();
                textBox_Brand.Text = dt.Rows[a][5].ToString();
                textBox_Model.Text = dt.Rows[a][6].ToString();
                textBox_Price.Text = dt.Rows[a][7].ToString();
                textBox_YiCai.Text = dt.Rows[a][8].ToString();
                textBox_WeiCai.Text = dt.Rows[a][9].ToString();
                textBox_Remarks.Text = dt.Rows[a][10].ToString();
            }
        }
    }
}
