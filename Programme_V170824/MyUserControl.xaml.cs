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
        //public string str;


        public int index = -1;//选中的行号索引
        private List<TextBox> textBoxlist;//存放数据改变的空间的名称
        public DataTable dt;//查询的datatable
        public string ChoiceNum;//选择的供应商序号
        public MyUserControl()
        {
            InitializeComponent();
            if(!init())
            {
                textBlock.Text += "初始化异常！\n";
            }
        }

      
        public bool init()
        {
            bool resulet = true;
            try
            {
                string str_sql = "Select *from Supplier";
                
                Helper.DBManager.DBConnect();
                dt = Helper.DBManager.DataHandle(str_sql, Helper.func.select) as DataTable;
                if (dt != null)
                {
                    textBlock.Text += "数据库操作成功！\n";
                    dataGrid.ItemsSource = dt.DefaultView;
                }

                string str_sqlcol = "Select name from syscolumns Where id=object_id('Supplier')";
                int i = 0;
                string[] comboboxitem = new string[15];
                Helper.DBManager.DBConnect();
                DataTable dt_col = Helper.DBManager.DataHandle(str_sqlcol, Helper.func.select) as DataTable;
                if(dt_col.Rows.Count == 0)
                {
                    textBlock.Text += "列名获得错误！\n";
                }
                foreach(DataRow dr in dt_col.Rows)
                {
                    comboboxitem[i] += dr[0].ToString();
                    i++;
                }
                comboBox.ItemsSource = comboboxitem;
                comboBox.SelectedIndex = 0;

                Helper.DBManager.DBConnectionClose();

            }catch(Exception ex)
            {
                resulet = false;
                textBlock.Text += ex.Message+"\n";
            }

            textBoxlist = new List<TextBox>();

            textBox_supNum.LostFocus += lostFocus;
            textBox_supName.LostFocus += lostFocus;
            textBox_supChannel.LostFocus += lostFocus;
            textBox_supLX.LostFocus += lostFocus;
            textBox_supTel.LostFocus += lostFocus;
            textBox_supAddress.LostFocus += lostFocus;
            textBox_supBank.LostFocus += lostFocus;
            textBox_supEmail.LostFocus += lostFocus;
            textBox_supWebsite.LostFocus += lostFocus;
            textBox_supRemarks.LostFocus += lostFocus;


            textBox_supNum.ToolTip = "供应商序号";
            textBox_supName.ToolTip = "公司名称";
            textBox_supChannel.ToolTip = "渠道";
            textBox_supLX.ToolTip = "联系人";
            textBox_supTel.ToolTip = "电话";
            textBox_supAddress.ToolTip = "地址";
            textBox_supBank.ToolTip = "开户行";
            textBox_supEmail.ToolTip = "[邮箱/QQ]";
            textBox_supWebsite.ToolTip = "网址";
            textBox_supRemarks.ToolTip = "备注";


            return resulet;

        }
       
        private void Selectbtn_Click(object sender, RoutedEventArgs e)
        {
            string sql_select = "Select * from Supplier where " + comboBox.SelectedValue.ToString() + " like '%" + textBox.Text + "%'";
            Helper.DBManager.DBConnect();
            dt = Helper.DBManager.DataHandle(sql_select, Helper.func.select) as DataTable;
            dataGrid.ItemsSource = dt.DefaultView;
            Helper.DBManager.DBConnectionClose();
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void Updatebtn_Click(object sender, RoutedEventArgs e)
        {
            string sql_updata = "Update Supplier set  录入人 = '" + Globle.User_Name + "',录入时间='" + DateTime.Now.ToString("yyyy-MM-dd") + "',供应商序号='" + textBox_supNum.Text + "'";


            foreach (var tb in textBoxlist)
            {
                sql_updata += "," + tb.ToolTip.ToString() + "= '" + tb.Text + "' ";
                tb.DataContext = null;
            }

            sql_updata += "where 供应商序号=" + textBox_supNum.Text;

            Helper.DBManager.DBConnect();
            Helper.DBManager.DataHandle(sql_updata, Helper.func.update);
            Helper.DBManager.DBConnectionClose();
            textBlock.Text += "更新成功！\n";

        }

        private void Deletebtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChoiceNum != null)
            {
                string sql_delete;
                sql_delete = "Delete from Supplier where 供应商序号=" + ChoiceNum;
                Helper.DBManager.DBConnect();
                Helper.DBManager.DataHandle(sql_delete, Helper.func.delete);
                dataGrid.SelectedIndex = -1;
                textBlock.Text += "删除成功！\n";
            }
        }

        private void Insertbtn_Click(object sender, RoutedEventArgs e)
        {
            if (ProductNumRepeat())
            {
                if (textBox_supNum.Text != null )
                {
                    string sql_insert = "Insert into Supplier(供应商序号,公司名称,渠道,联系人,电话,地址,开户行,[邮箱/QQ],网址,录入人,录入时间,备注) values('" + textBox_supNum.Text + "','" + textBox_supName.Text + "','" + textBox_supChannel.Text + "','" + textBox_supLX.Text + "','" + textBox_supTel.Text + "','" + textBox_supAddress.Text + "','" + textBox_supBank.Text + "','" + textBox_supEmail.Text + "','" + textBox_supWebsite.Text + "','" + Globle.User_Name + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + textBox_supRemarks.Text + "')";
                    Helper.DBManager.DBConnect();
                    Helper.DBManager.DataHandle(sql_insert, Helper.func.insert);
                    Helper.DBManager.DBConnectionClose();
                    textBlock.Text += "添加成功！\n";
                }


            }
            else
            {
                textBlock.Text += "供应商序号重复！\n";
            }

        }

        public void lostFocus(object sender,EventArgs e)
        {
            var name = sender as TextBox;
            textBlock.Text += name.Name + "\n";
            
                foreach (var tb in textBoxlist)
                {
                    if (tb == name)
                        return;
                }
            
            textBoxlist.Add(name);
        }


            /// <summary>
            /// 供应商序号查重
            /// </summary>
            /// <returns></returns>
        public bool ProductNumRepeat()
        {
            DataTable dtrepeat;
            bool result = false;
            string selectstr = textBox_supNum.Text;
            
            string str_sql = "select * from Supplier where 供应商序号  = '" + textBox_supNum.Text + "'";
            Helper.DBManager.DBConnect();
            dtrepeat = Helper.DBManager.DataHandle(str_sql, Helper.func.select) as DataTable;
            if (dtrepeat.Rows.Count == 0)//datatable不为空则已存在产品序号不能重复添加
                result = true;

            return result;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            index = dataGrid.SelectedIndex;

            if (index < dt.Rows.Count)
            {
                if (index >= 0)
                {
                    textBlock.Text += "选中行号为" + index.ToString() + "\n";
                    ChoiceNum = dt.Rows[index][0].ToString();
                    textBox_supNum.Text = dt.Rows[index][0].ToString();
                    textBox_supName.Text = dt.Rows[index][1].ToString();
                    textBox_supChannel.Text = dt.Rows[index][2].ToString();
                    textBox_supLX.Text = dt.Rows[index][3].ToString();
                    textBox_supTel.Text = dt.Rows[index][4].ToString();
                    textBox_supAddress.Text = dt.Rows[index][5].ToString();
                    textBox_supBank.Text = dt.Rows[index][6].ToString();
                    textBox_supEmail.Text = dt.Rows[index][8].ToString();
                    textBox_supWebsite.Text = dt.Rows[index][9].ToString();
                    textBox_supRemarks.Text = dt.Rows[index][12].ToString();
                    textBlock1.Text = "录入人：" + dt.Rows[index][10].ToString() + "\n录入时间：" + dt.Rows[index][11].ToString() + "\n";

                }
                else
                {
                    textBox_supNum.Text = " ";
                    textBox_supName.Text = " ";
                    textBox_supChannel.Text = " ";
                    textBox_supLX.Text = " ";
                    textBox_supTel.Text = " ";
                    textBox_supAddress.Text = " ";
                    textBox_supBank.Text = " ";
                    textBox_supEmail.Text = " ";
                    textBox_supWebsite.Text = " ";
                    textBox_supRemarks.Text = " ";
                    textBlock1.Text = "录入人：" + " " + "\n录入时间：" + " " + "\n";
                }
            }

        }
    }
}
