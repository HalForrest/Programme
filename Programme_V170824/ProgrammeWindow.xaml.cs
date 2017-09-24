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
using System.Threading;


namespace Programme_V170824
{
    /// <summary>
    /// ProgrammeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgrammeWindow : Window
    {
        private bool isok = true;//避免重复点击下载出现异常


        public ProgrammeWindow()
        {
            InitializeComponent();
            string path = Directory.GetCurrentDirectory();
            mediaElement.Source = new Uri(path + "//Image//loading.gif", UriKind.Absolute);
            mediaElement.Stop();
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            filePath = "D://" + floderName + "//";
            if(isok)
             Download();
        }

        string Username = "gzb1";
        string Password = "GZB2014.0";
        string ftpServerIP = "118.178.131.196:5001/冯帅帅";
        string floderName = "root";
        string fileName = "vcredist_x86.exe";
        string filePath;
        public void Download()
        {
            isok = false;
            loadingStart();
            FtpWebRequest reqFTP;    
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
            reqFTP.Credentials = new NetworkCredential(Username, Password);
            reqFTP.UseBinary = true;
            reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
            FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
            long le = reqFTP.ContentLength;
            ThreadPool.QueueUserWorkItem((obj) =>
           {
                
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                long progressBarValue = 0;
                int bufferSize = 1024;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {

                    outputStream.Write(buffer, 0, readCount);
                    progressBarValue += readCount;
                //    pbDown.Dispatcher.BeginInvoke(new ProgressBarSetter(SetProgressBar), progressBarValue);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);

                }    
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                isok = true;
               inv();//跨线程调用mediaelement
                },null);


           
       //     MessageBox.Show("下载完成！");







        }

        /// <summary>
        /// 声明委托 用于跨线程调用控件
        /// </summary>
        private delegate void mydelegate();
        private void inv()
        {
            mediaElement.Dispatcher.Invoke(new mydelegate(loadingStop));
        }


        /// <summary>
        /// 下载结束  结束播放动画并隐藏控件
        /// </summary>
        private void loadingStop()
        {
            
            mediaElement.Stop();
            mediaElement.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// 下载开始 开始播放gif
        /// </summary>
        private void loadingStart()
        {
            mediaElement.Visibility = Visibility.Visible;
            mediaElement.Play();    
        }
       
        /// <summary>
        /// 播放结束后循环
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            //mediaElement.Stop();
            mediaElement.Position = TimeSpan.MinValue;
            mediaElement.Play();
         
        }
    }
  

 


}


