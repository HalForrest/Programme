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

namespace Programme_V170824
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// 用做测试窗口
    /// 方案窗口也做成跟产品一样的界面  侧面
    /// </summary>
    public partial class Window1 : Window
    {
        string ftpServerIP = "118.178.131.196:5001/冯帅帅";
        string ftpUserID = "gzb1";
        string ftpPassword = "GZB2014.0";
        public Window1()
        {
            InitializeComponent();


        }
        /// <summary>
        /// 上传
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_up_Click(object sender, RoutedEventArgs e)
        {
            FtpUpDown ftp = new FtpUpDown(ftpServerIP, ftpUserID, ftpPassword);
            string localpath = @"D:\root\vcredist_x86.exe";//本地路径
            string ftppath = ftpServerIP + @"\test";
            bool bol = ftp.Upload(localpath, ftppath);
            if (bol == true)
                MessageBox.Show("上传成功");
            else
                MessageBox.Show("上传失败");

        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_down_Click(object sender, RoutedEventArgs e)
        {
            string errorinfo;
            string localpath = @"D:\root\f1";
            string filename = "vcredist_x86.exe";
            FtpUpDown ftp = new FtpUpDown(ftpServerIP, ftpUserID, ftpPassword);
            bool bol = ftp.Download(localpath, filename, out errorinfo);
            if (bol == true)
                MessageBox.Show("下载成功");
            else
                MessageBox.Show("下载失败：" + errorinfo + "");
        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_filelist_Click(object sender, RoutedEventArgs e)
        {
            FileDetail("test");
        }


        public void FileDetail(string floderName)
        {
            FtpUpDown ftpUpDown = new FtpUpDown(ftpServerIP, ftpUserID, ftpPassword);
            Paragraph pa = new Paragraph();
            Run r = new Run();
            string[] str = ftpUpDown.GetFileList(floderName);
            if (str != null)
            {
                foreach (string item in str)
                {
                    TreeViewItem treeviewitem1 = new TreeViewItem();
                    treeviewitem1.Header = item;
                  //？？  treeView.Items.Add(treeviewitem1);
                    r.Text = item;
                    textBox.AppendText(r.Text + "\n");
                }
            }
            string[] filedetaillist = ftpUpDown.GetFilesDetailList(floderName);
            if(filedetaillist == null)
            {
                return;
            }
            foreach (string mystr in filedetaillist)
            {
                if (mystr.Contains("<DIR>"))
                {
                    // FileDetail(mystr);
                     MessageBox.Show(mystr);
                }
                    
               // textBox.AppendText(mystr + "\n");
            }
        }

    }
}
