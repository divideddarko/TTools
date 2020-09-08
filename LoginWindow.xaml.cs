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
using TTools.Logging;

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
                    this.Close();
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
            string emailPatternAllowance = @"";

            Match userName = Regex.Match(usernameRegister.Text, inputPatternAllowance);
            Match emailAddress = Regex.Match(emailRegiser.Text, emailPatternAllowance);

            // Check that all feilds have been completed

            if (!(emailAddress.Success))
            {
                emailRegResults.Content = $"Please check the email address";
            }

            if (string.IsNullOrWhiteSpace(usernameRegister.Text) || !(userName.Success))
            {
                errors.Add("Please check the username");
                usernameRegResults.Content = $"Check your username matches AlphaNumeric Characters";
            }

            if (string.IsNullOrWhiteSpace(regPassOne.Password))
            {
                errors.Add("Please enter a password");
                passOneRegResults.Content = "Please check that your password match";
            }

            if (regPassOne.Password != regPassTwo.Password || string.IsNullOrWhiteSpace(regPassTwo.Password))
            {
                errors.Add("Please ensure that your passwords match");
                passTwoRegResults.Content = "Please check that your password match";

            }

            if (errors.Count > 0)
            {
                // errorReports("Please check your registration details", errors);

            }
            else
            {
                MessageBox.Show("Welcome to the party");
                int check = checkUserName(emailRegister.Text);

                if (check < 1)
                {
                    insertNewUser(usernameRegister.Text, regPassOne.Password, emailRegister.Text);
                }
            }
        }

        private int checkUserName(string email)
        {
            int i = 0;
            // Check to see if the username already exists.
            SQLConnection cmd = new SQLConnection();
            SqlDataReader reader = cmd.GetReader("SELECT * FROM Users WHERE email = @0", email);

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    i += 1;
                }
                emailRegResults.Content = "This email is already in use";
            }

            reader.Close();
            cmd.CloseConnection();

            return i;
        }

        private void insertNewUser(string username, string password, string email)
        {
            try
            {
                SQLConnection cmd = new SQLConnection();
                SqlDataAdapter reader = cmd.CreateSet("INSERT INTO Users (username, accountTypeId, password, email) VALUES (@0, @1, @2, @3)", username, "2", password, email);
                cmd.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an issue, please try again later");
                Reporting.SQLReporting("Error", $"Testing the reporting system {ex}");
            }

            MessageBox.Show("Successfully Signed up");
        }

        private void loginBtn(object sender, RoutedEventArgs e)
        {
            int results = checkUserLogin(usernameLogin.Text, passwordLogin.Password);

            if (results > 0)
            {
                MainWindow mw = new MainWindow();
                mw.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("PLease check your login credentials");
            }

        }

        private int checkUserLogin(string username, string password)
        {
            int i = 0;
            SQLConnection cmd = new SQLConnection();
            SqlDataReader reader = cmd.GetReader("SELECT username, password FROM TTools.dbo.Users WHERE username = @0 AND password = @1", username, password);

            if (reader.HasRows)
            {
                i += 1;
            }

            reader.Close();
            cmd.CloseConnection();

            return i;
        }
    }
}