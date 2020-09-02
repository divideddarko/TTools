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

namespace TTools
{
    public partial class LoginWindow : Window
    {
        public static void MainLogin()
        {
            // InitializeComponent();
        }

        private void titleBarWindowController(object sender, RoutedEventArgs e)
        {

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

        private void loginBtn(object sender, RoutedEventArgs e)
        {
            // MessageBox.Show("Logged in");
            SQLConnection cmd = new SQLConnection();
            SqlDataReader reader = cmd.GetReader("SELECT * FROM TTools.dbo.Users");

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MessageBox.Show(reader[0].ToString());
                }
            }
            reader.Close();
            cmd.CloseConnection();
        }
    }
}