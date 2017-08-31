using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace Programme_V170824.Helper
{
    static class DBManager
    {
        // 链接数据库
            // 关闭连接
            // 数据查询
        public static SqlConnection Sqlconn;

        public static void DBConnect(string Server = "118.178.131.196", string Database = "DBgzb1", string userid = "gzb1", string pwd = "gzb123456")
        {
            string con = "Server='" + Server + "';Database='" + Database + "';user id='" + userid + "';pwd='" + pwd + "'";
            Sqlconn = new SqlConnection(con);
        }
        public static void DBConnectionClose()
        {
            Sqlconn.Close();
        }
        public static DataTable dt;
        public static void DataHandle(string str_Sql,func f)
        {
             dt= new DataTable();

            switch (f)
            {
                case func.select:
                    using (SqlConnection sqlconn_select = Sqlconn)
                    {
                        sqlconn_select.Open();
                        SqlDataAdapter sda = new SqlDataAdapter(str_Sql, sqlconn_select);
                        
                        sda.Fill(dt);
                        //dataGrid.ItemsSource = dt.DefaultView;
                        sqlconn_select.Close();

                    
                    }
                        break;
                case func.insert:
                    break;
                case func.delete:
                    break;
                case func.update:
                    break;

            }
        }

    }

    
}
