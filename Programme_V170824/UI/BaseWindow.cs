using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace CommonLib
{
    public class BaseWindow : Window
    {
        ResourceDictionary style1; 
        bool closeStoryBoardCompleted = false;
        DoubleAnimation closeAnimation1;

        public BaseWindow()
        {
            style1 = new ResourceDictionary();
            style1.Source = new Uri("CommonLib;component/UI/BaseWindowStyle.xaml", UriKind.Relative);
            InitializeStyle();
            this.Loaded += delegate
            {
                InitializeEvent();
            };
            this.Closing += new System.ComponentModel.CancelEventHandler(BaseWindow_Closing);
        }

        private void InitializeStyle()
        {
            this.Style = (System.Windows.Style)style1["BaseWindowStyle"];
        }

        private void InitializeEvent()
        {
            ControlTemplate baseWindowTemplate1 = (ControlTemplate)style1["BaseWindowControlTemplate"];

            //Button minBtn = (Button)baseWindowTemplate.FindName("btnMin", this);
            //minBtn.Click += delegate
            //{
            //    this.WindowState = WindowState.Minimized;
            //};

            //Button maxBtn = (Button)baseWindowTemplate.FindName("btnMax", this);
            //maxBtn.Click += delegate
            //{
            //    this.WindowState = (this.WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal);
            //};

            Button closeBtn = (Button)baseWindowTemplate1.FindName("btnClose", this);
            closeBtn.Click += delegate
            {
                this.Close();
            };

            Border borderTitle = (Border)baseWindowTemplate1.FindName("borderTitle", this);
            borderTitle.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };
            //borderTitle.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
            //{
            //    if (e.ClickCount >= 2)
            //    {
            //        maxBtn.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            //    }
            //};
        }

        public void BaseWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!closeStoryBoardCompleted)
            {
                closeAnimation1 = new DoubleAnimation();
                closeAnimation1.From = 1;
                closeAnimation1.To = 0;
                closeAnimation1.Duration = new Duration(TimeSpan.Parse("0:0:0.4"));
                closeAnimation1.Completed += new EventHandler(closeWindow_Completed);
                ScaleTransform st = new ScaleTransform();

                st.CenterY = this.Height / 2;
                this.RenderTransform = st;
                st.BeginAnimation(ScaleTransform.ScaleYProperty, closeAnimation1);
                e.Cancel = true;
            }
        }

        void closeWindow_Completed(object sender, EventArgs e)
        {
            closeStoryBoardCompleted = true;
            this.Close();
        }
    }
}
