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
using CommonLib;

namespace Programme_V170824
{
    /// <summary>
    /// ProductWindow.xaml 的交互逻辑
    /// </summary>

    public partial class ProductWindow : BaseWindow
    {
        public ProductWindow()
        {
            InitializeComponent();
            LoginTime.Content += DateTime.Now.ToString();
            mainMenu.Text = "产品管理";
           
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // btn_Repair_Click(null, null);
            
        }
        /// <summary>
        /// 获取渐变效果
        /// </summary>
        /// <returns></returns>
        private LinearGradientBrush GetLinearGradientBrush()
        {
            LinearGradientBrush linearGradient = new LinearGradientBrush();
            linearGradient.StartPoint = new System.Windows.Point(0, 0.5);
            linearGradient.EndPoint = new System.Windows.Point(1, 0.5);
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0057bf"), 0.0));
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#4b8acf"), 0.1));
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#e2ecf6"), 0.3));
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#ffffff"), 0.5));
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#e2ecf6"), 0.8));
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#4b8acf"), 0.1));
            linearGradient.GradientStops.Add(new GradientStop((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#0057bf"), 1.0));
            return linearGradient;
        }
        private void SetButtonDefaultBackgroud()
        {
            LogicTree(stackPanel1);
        }
        private void LogicTree(object obj)
        {
            if (!(obj is DependencyObject))
            {
                return;
            }
            foreach (object child in LogicalTreeHelper.GetChildren(obj as DependencyObject))
            {
                if (child is Button)
                {
                    Button btn = (Button)child;
                    btn.Background = System.Windows.Media.Brushes.Transparent;
                }
                LogicTree(child);
            }
        }
        private double originalHeight = 0.0;
        private void btn_customer_Click(object sender, RoutedEventArgs e)
        {
            originalHeight = stackPanelRight.ActualHeight;

            SetButtonDefaultBackgroud();
            Button btn = e.OriginalSource as Button;
            btn.Background = GetLinearGradientBrush();
            UserControl userControl = null;
            stackPanelRight.Children.Clear();
            if (btn.Content.ToString().Equals("1"))
            {
                 userControl = new MyUserControl("1");
            }
            if(btn.Content.ToString().Equals("2"))
            {
                userControl = new MyUserControl2();
            }
            if (btn.Content.ToString().Equals("3"))
            {
                userControl = new MyUserControl("3");
            }
            if (btn.Content.ToString().Equals("4"))
            {
                userControl = new MyUserControl("4");
            }
            stackPanelRight.Children.Add(userControl);
        }

        //主界面窗口大小的变化
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                //  CustomerData.AddHeight = (stackPanelRight.ActualHeight - originalHeight) / 3 + 35.0;
            }
            else
            {
                // CustomerData.AddHeight = 35.0;
            }

        }

        private void btn_Repair_Click(object sender, RoutedEventArgs e)
        {
           /* originalHeight = stackPanelRight.ActualHeight;

            SetButtonDefaultBackgroud();
            Button btn = btn_Repair;
            btn.Background = GetLinearGradientBrush();
            UserControl userControl = null;
            stackPanelRight.Children.Clear();
            if (btn.Content.ToString().Equals("客户维修"))
            {
                userControl = new MyUserControl();
            }
            stackPanelRight.Children.Add(userControl);*/
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //供应商
            mainMenu.Text = "供应商管理";
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //产品
            mainMenu.Text = "产品管理";
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            //帮助
            MessageBox.Show("QQ：2121274301 \n 暗号：帅帅哥，来帮忙啊", "Help", MessageBoxButton.OK);
        }
    }
}
 
 