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
        public static SqlConnection Sqlconn;//程序中链接的数据库都是一样的 

        public static void DBConnect(string Server = "118.178.131.196", string Database = "DBgzb1", string userid = "gzb1", string pwd = "gzb123456")
        {
            string con = "Server='" + Server + "';Database='" + Database + "';user id='" + userid + "';pwd='" + pwd + "'";
            Sqlconn = new SqlConnection(con);
        }
        public static void DBConnectionClose()
        {
            Sqlconn.Close();
        }
       // public static DataTable dt;
       /// <summary>
       /// 数据库的操作
       /// </summary>
       /// <param name="str_Sql"></param>
       /// <param name="f"></param>
       /// <returns>根据不同的语句对不同的表进行操作</returns>
        public static DataTable DataHandle(string str_Sql,func f)
        {
            DataTable dt= new DataTable();

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
            return dt;
        }

    }

    
}
