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
        public static object DataHandle(string str_Sql,func f)
        {
            DataTable dt= new DataTable();
            int i = 0;//受影响行数

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
                    return dt;
                       
                case func.insert:
                    using (SqlConnection sqlconn_insert = Sqlconn)
                    {
                        sqlconn_insert.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = sqlconn_insert;
                        com.CommandType = CommandType.Text;
                        com.CommandText = str_Sql;
                        SqlDataReader sdr = com.ExecuteReader();
                        sdr.Close();
                        sqlconn_insert.Close();
                    }
                        break;
                case func.delete:
                    using (SqlConnection sqlconn_delete = Sqlconn)
                    {
                        sqlconn_delete.Open();
                        SqlCommand com = new SqlCommand();
                        com.Connection = sqlconn_delete;
                        com.CommandType = CommandType.Text;
                        com.CommandText = str_Sql;
                        SqlDataReader sdr = com.ExecuteReader();
                        sdr.Close();
                        sqlconn_delete.Close();
                    }
                        break;
                case func.update:
                    using (SqlConnection sqlconn_update = Sqlconn)
                    {
                        sqlconn_update.Open();
                        SqlCommand com = new SqlCommand(str_Sql, sqlconn_update);
                        i= com.ExecuteNonQuery();
                        sqlconn_update.Close();
                    }
                        break;

            }
            return null;
        }

    }

    
}
