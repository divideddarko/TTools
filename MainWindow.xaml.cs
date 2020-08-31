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
using System.Diagnostics;

namespace TTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        void moveWindow()
        {
            this.DragMove();
        }

        void resizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if ((this.WindowState).ToString() != "Maximized")
                {
                    this.WindowState = WindowState.Maximized;
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                }
            }
        }

        void WindowController(object sender, RoutedEventArgs e)
        {

            Button values = sender as Button;
            string senderName = values.Name;

            switch (senderName)
            {
                case "minimize":
                    this.WindowState = WindowState.Minimized;
                    break;
                case "resize":
                    if ((this.WindowState).ToString() != "Maximized")
                    {
                        this.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        this.WindowState = WindowState.Normal;
                    }
                    break;
                case "close":
                    System.Windows.Application.Current.Shutdown();
                    break;
                default:
                    break;
            }
        }

        private void openMenu(object sender, RoutedEventArgs e)
        {
            // Button values = sender as Button;

            // string senderName = values.Name;

            // ImageBrush ib = new ImageBrush();
            // ib.ImageSource = new BitmapImage(new Uri(@"open-menuopen.png", UriKind.Relative));

            // values.Background = ib;




            MessageBox.Show("Hello");
        }

        private void OnDragMoveWindow(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
