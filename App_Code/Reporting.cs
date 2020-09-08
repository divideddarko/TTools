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

namespace TTools.Logging
{
    public class Reporting
    {

        public static void errorHandler()
        {
            // function to deal with both local reporting and SQL Reporting
        }
        public void localReporting(string errorMsg)
        {
            // Needs to take in a known local directory
            // Local reporting non tech issues maybe logs of the system and time of error less important but vital for debugging more specific errors

        }

        public static void SQLReporting(string errorType, string errorMsg)
        {
            //  Will need to have tables setup on the server
            // Showing any errors that might have occured when testing

            // DateTime d = DateTime.Now;

            try
            {
                SQLConnection cmd = new SQLConnection();
                SqlDataAdapter reader = cmd.CreateLoggingSet("errReporting", "INSERT INTO ErrorLogging (username, errorType, errorMsg) VALUES (@0, @1, @2)", "divideddarko", errorType, errorMsg);
                cmd.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"error : {ex}");
            }
        }
    }
}