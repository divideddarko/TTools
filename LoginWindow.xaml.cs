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
using TTools.DBConn;
using Microsoft.Data.SqlClient;
using TTools.Windowsconfiguration;

namespace TTools
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            registerGrid.Opacity = 0;
            LoginGrid.Opacity = 1;
        }

        private void titleBarWindowController(object sender, MouseButtonEventArgs e)
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

            DragMove();
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

        public void registerAccountBtn(object sender, RoutedEventArgs e)
        {
            // Update form for Register form.
            int visibility = (LoginGrid.Opacity == 0) ? 1 : 0;
            int Regvisibility = (registerGrid.Opacity == 0) ? 1 : 0;

            LoginGrid.Opacity = visibility;
            registerGrid.Opacity = Regvisibility;
        }

        private void loginBtn(object sender, RoutedEventArgs e)
        {

            SQLConnection cmd = new SQLConnection();
            SqlDataReader reader = cmd.GetReader("SELECT * FROM TTools.dbo.Users");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MessageBox.Show(reader[1].ToString());
                }
            }
            reader.Close();
            cmd.CloseConnection();
        }
    }
}