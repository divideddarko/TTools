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
        }

        private void titleBarWindowController(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        void WindowController(object sender, RoutedEventArgs e)
        {
            Button values = sender as Button;
            string senderName = values.Name;

            // switch (senderName)
            // {
            this.Close();
            // }
        }

        // toggleView changes the scene from New Account / Login / Reset Password
        public void toggleView(object sender, RoutedEventArgs e)
        {
            Hyperlink values = sender as Hyperlink;
            string hyperName = values.Name;

            windowStyle ws = new windowStyle();
            ws.opacitySettings(hyperName, LoginGrid, RegisterGrid, PasswordGrid);
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

        public void emailPass(object sender, RoutedEventArgs e)
        {

        }

        public void registerAccountBtn(object sender, RoutedEventArgs e)
        {
            List<string> errors = new List<string>();

            string inputPatternAllowance = @"^[A-Za-z0-9_.]+$";
            string emailPatternAllowance = @"^[\w+-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            Match userName = Regex.Match(usernameRegister.Text, inputPatternAllowance);
            Match emailAddress = Regex.Match(emailRegister.Text, emailPatternAllowance);

            // Check that all feilds have been completed

            if (!(emailAddress.Success))
            {
                emailRegResults.Content = $"Please check the email address";
            }
            else
            {
                emailRegResults.Content = "";
            }

            if (string.IsNullOrWhiteSpace(usernameRegister.Text) || !(userName.Success))
            {
                errors.Add("Please check the username");
                usernameRegResults.Content = $"Check your username matches AlphaNumeric Characters";
            }
            else
            {
                usernameRegResults.Content = "";
            }

            if (string.IsNullOrWhiteSpace(regPassOne.Password))
            {
                errors.Add("Please enter a password");
                passOneRegResults.Content = "Please check that your password match";
            }
            else
            {
                passOneRegResults.Content = "";
            }

            if (regPassOne.Password != regPassTwo.Password || string.IsNullOrWhiteSpace(regPassTwo.Password))
            {
                errors.Add("Please ensure that your passwords match");
                passTwoRegResults.Content = "Please check that your password match";
            }
            else
            {
                passTwoRegResults.Content = "";
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