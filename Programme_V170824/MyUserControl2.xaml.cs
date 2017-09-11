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
        public DataTable dt;//存放数据库的内容数据
        public DataTable dt_columns;//存放数据库表的列名
        //public bool l = false;//
        public int a;//选择的索引
        private List<TextBox> textBoxlist;//存放数据改变的空间的名称
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
            dt_columns = Helper.DBManager.DataHandle(str_sql,Helper.func.select) as DataTable ;
            int i = 0;
            if(dt_columns==null)
            {
                MessageBox.Show("数据库错误！");
            }
            textBox.Text += "数据库连接成功！\n";
            foreach( DataRow dr in dt_columns.Rows)
            {
                ProductRowName[i] = dr[0].ToString();
                i++;
            }
            SelectcomboBox.ItemsSource = ProductRowName;
            SelectcomboBox.SelectedIndex = 0;

            textBoxlist = new List<TextBox>();
            /*本来想用textchange事件，但效果不好 改为lostfocus 事件还是mytextchangge
                        textBox_ProductNum.TextChanged += MyTextChanged;
                        textBox_Address.TextChanged += MyTextChanged;
                        textBox_Brand.TextChanged += MyTextChanged;
                        textBox_Model.TextChanged += MyTextChanged;
                        textBox_Name.TextChanged += MyTextChanged;
                        textBox_Para.TextChanged += MyTextChanged;
                        textBox_Price.TextChanged += MyTextChanged;

                        textBox_Remarks.TextChanged += MyTextChanged;
                        textBox_YiCai.TextChanged += MyTextChanged;
                        textBox_ZhengShu.TextChanged += MyTextChanged;
            */
         //   textBox_ProductNum.LostFocus += MyTextChanged;
            textBox_Address.LostFocus+= MyTextChanged;
            textBox_Brand.LostFocus += MyTextChanged;
            textBox_Model.LostFocus += MyTextChanged;
            textBox_Name.LostFocus += MyTextChanged;
            textBox_Para.LostFocus += MyTextChanged;
            textBox_Price.LostFocus += MyTextChanged;

            textBox_Remarks.LostFocus += MyTextChanged;
            textBox_YiCai.LostFocus += MyTextChanged;
            textBox_ZhengShu.LostFocus += MyTextChanged;

            textBox_Brand.ToolTip = "品牌";
            textBox_Address.ToolTip = "产地";
            textBox_Model.ToolTip = "型号";
            textBox_Name.ToolTip = "产品名";
            textBox_Para.ToolTip = "技术参数";
            textBox_Price.ToolTip = "单价";
            textBox_Remarks.ToolTip = "备注";
            textBox_YiCai.ToolTip = "采购状况";
            textBox_ZhengShu.ToolTip = "证书";
            

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
            dt = Helper.DBManager.DataHandle(str_sql, Helper.func.select) as DataTable;
            dataGrid.ItemsSource = dt.DefaultView;
        }
        string ChoiceLine;

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            a= dataGrid.SelectedIndex;
            if (a < dt.Rows.Count)//点击空白行
            {
                if (a >= 0)
                {
                    textBox.Text += "选中行号为" + dataGrid.SelectedIndex.ToString() + "\n";
                    textBox.Text += "值为：" + dt.Rows[a][0].ToString();

                    /*
                                    string[] str = new string[15];
                                    //int i = 0;
                                    for (int i = 0; i < 11; i++)
                                    {
                                        str[i] = dt.Rows[a][i].ToString();
                                    }
                                    */
                    ChoiceLine = dt.Rows[a][0].ToString();
                    textBox_ProductNum.Text = dt.Rows[a][0].ToString();
                    comboBox_SupplierNum.Text = dt.Rows[a][1].ToString();
                    textBox_Name.Text = dt.Rows[a][2].ToString();
                    textBox_Para.Text = dt.Rows[a][3].ToString();
                    textBox_Address.Text = dt.Rows[a][4].ToString();
                    textBox_Brand.Text = dt.Rows[a][5].ToString();
                    textBox_Model.Text = dt.Rows[a][6].ToString();
                    textBox_Price.Text = dt.Rows[a][7].ToString();
                    textBox_YiCai.Text = dt.Rows[a][8].ToString();
                    textBox_ZhengShu.Text = dt.Rows[a][9].ToString();
                    textBox_Remarks.Text = dt.Rows[a][10].ToString();
                }
                else
                {
                    ChoiceLine = " ";
                    textBox_ProductNum.Text = " ";
                    comboBox_SupplierNum.Text = " ";
                    textBox_Name.Text = " ";
                    textBox_Para.Text = " ";
                    textBox_Address.Text = " ";
                    textBox_Brand.Text = " ";
                    textBox_Model.Text = " ";
                    textBox_Price.Text = " ";
                    textBox_YiCai.Text = " ";
                    textBox_ZhengShu.Text = " ";
                    textBox_Remarks.Text = " ";
                }
               
            }
        }

        private void Updatabtn_Click(object sender, RoutedEventArgs e)
        {//更新加上录入人和录入时间

            string sql_updata = "Update Product set  录入人 = '"+Globle.User_Name+"',录入时间='"+DateTime.Now.ToString("yyyy-MM-dd")+"',供应商序号='" + comboBox_SupplierNum.Text + "'";


            foreach (var tb in textBoxlist)
            {
                sql_updata += ","+ tb.ToolTip.ToString() + "= '" + tb.Text + "' ";
                tb.DataContext = null;
            }

            sql_updata +=   "where 产品序号=" + textBox_ProductNum.Text;

            Helper.DBManager.DBConnect();
            Helper.DBManager.DataHandle(sql_updata, Helper.func.update);

            Helper.DBManager.DBConnectionClose();
            textBox.Text += "更新成功！\n";


        }

     


        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChoiceLine != null)
            {
                string sql_delete;
                sql_delete = "Delete from Product where 产品序号=" + ChoiceLine;
                Helper.DBManager.DBConnect();
                Helper.DBManager.DataHandle(sql_delete, Helper.func.delete);
                dataGrid.SelectedIndex = -1;
            }


        }


       

      

        private void comboBox_SupplierNum_KeyDown_1(object sender, KeyEventArgs e)
        {
            textBox.Text += "text  change\n";
            DataTable dt_sup;
            string sql_suppselect;
            sql_suppselect = "select 供应商序号 from Supplier where 供应商序号 like '%" + comboBox_SupplierNum.Text + "%'";
            Helper.DBManager.DBConnect();
            dt_sup = Helper.DBManager.DataHandle(sql_suppselect, Helper.func.select) as DataTable;
            List<string> suplist = new List<string>();
            foreach (DataRow dr in dt_sup.Rows)
            {
                suplist.Add(dr[0].ToString());
            }

            comboBox_SupplierNum.ItemsSource = suplist;
          

        }
        

        private void MyTextChanged(object sender, EventArgs e)
        {

          
                var name = sender as TextBox;
                textBox.Text += name.Name + "\n";
                foreach (var tb in textBoxlist)
                {
                    if (tb == name)
                        return;
                } 
                textBoxlist.Add(name);
            
        }

        private void Insertbtn_Click(object sender, RoutedEventArgs e)
        {
            //1.产品序号查重
            //2.信息添加
            if(ProductNumRepeat())
            {
                if (textBox_ProductNum.Text != null && comboBox_SupplierNum.Text != null)
                {
                    string sql_insert = "Insert into Product(产品序号,供应商序号,产品名,技术参数,产地,品牌,型号,单价,采购状况,证书,录入人,录入时间,备注) values('"+textBox_ProductNum.Text+"','"+comboBox_SupplierNum.Text+"','"+textBox_Name.Text+"','"+textBox_Para.Text+"','"+textBox_Address.Text+"','"+textBox_Brand.Text+"','"+textBox_Model.Text+"','"+textBox_Price.Text+"','"+textBox_YiCai.Text+"','"+textBox_ZhengShu.Text+"','"+Globle.User_Name+"','"+DateTime.Now.ToString("yyyy-MM-dd")+"','"+textBox_Remarks.Text+"')";
                    Helper.DBManager.DBConnect();
                    Helper.DBManager.DataHandle(sql_insert, Helper.func.insert);
                    textBox.Text += "添加成功！\n";
                }
                    

            }else
            {
                textBox.Text += "产品序号重复！\n";
            }

        }
        public bool ProductNumRepeat()
        {
            DataTable dtrepeat;
            bool result = false ;
            string selectstr = textBox_ProductNum.Text.ToString();
            
            string str_sql = "select * from Product where 产品序号  = '" + textBox_ProductNum.Text + "'";
            Helper.DBManager.DBConnect();
            dtrepeat = Helper.DBManager.DataHandle(str_sql, Helper.func.select) as DataTable;
            if (dtrepeat.Rows.Count == 0)//datatable不为空则已存在产品序号不能重复添加
                result = true ;

            return result;
        }
    }
}
