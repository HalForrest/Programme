using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using System.IO;

namespace Programme_V170824
{
    /// <summary>
    /// ProgrammeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgrammeWindow : Window
    {
        public ProgrammeWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Download("D://" + floderName + "//" , fileName);

        }

        string Username = "gzb1";
        string Password = "GZB2014.0";
        string ftpServerIP = "118.178.131.196:5001/冯帅帅";
        string floderName = "root";
        string fileName = "vcredist_x86.exe";
        public void Download(string filePath,string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(Username, Password);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);

                }
                MessageBox.Show("下载完成！");
                ftpStream.Close();
                outputStream.Close();
                response.Close();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
