using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public void toggleRegLoginBtn(object sender, RoutedEventArgs e)
        {
            int visibility = (LoginGrid.Opacity == 0) ? 1 : 0;
            int Regvisibility = (registerGrid.Opacity == 0) ? 1 : 0;

            LoginGrid.Opacity = visibility;
            registerGrid.Opacity = Regvisibility;
        }

        public void errorReports(string headlineErrorMessage, List<string> errors)
        {
            string er = "";
            // break error messages into readable format
            foreach (string e in errors)
            {
                er += $"{e}\n\r";
            }

            MessageBox.Show($"{headlineErrorMessage} \n\r{er}");
        }

        public void registerAccountBtn(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();

            string inputPatternAllowance = @"^[A-Za-z0-9_.]+$";

            Match userName = Regex.Match(usernameRegister.Text, inputPatternAllowance);

            // Check that all feilds have been completed
            if (string.IsNullOrWhiteSpace(usernameRegister.Text) || !(userName.Success))
            {
                errors.Add("Please check the username");
            }

            if (string.IsNullOrWhiteSpace(regPassOne.Password))
            {
                errors.Add("Please enter a password");
            }

            if (regPassOne.Password != regPassTwo.Password || string.IsNullOrWhiteSpace(regPassTwo.Password))
            {
                errors.Add("Please ensure that your passwords match");
            }

            if (errors.Count > 0)
            {
                errorReports("Please check your registration details", errors);
            }
            else
            {
                MessageBox.Show("Welcome to the party");
                insertNewUser(usernameRegister.Text, regPassOne.Password);
            }
        }


        private void insertNewUser(string username, string password)
        {
            // Check to see if the username already exists.
            try
            {
                SQLConnection cmd = new SQLConnection();
                SqlDataReader reader = cmd.GetReader("SELECT * FROM Users WHERE username = @0", username);

                if (reader.HasRows)
                {
                    MessageBox.Show("Username already taken");
                    return;
                }
                reader.Close();
                cmd.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error, please try again\n\r{ex}");
            }


            try
            {
                SQLConnection cmd = new SQLConnection();
                SqlDataAdapter reader = cmd.CreateSet("INSERT INTO Users (username) VALUES (@0)", username);
                cmd.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue, please try again later");
            }

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