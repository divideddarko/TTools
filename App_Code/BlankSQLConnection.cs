using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TTools.DBConn
{
    public class SQLConnection
    {
        private SqlConnection conn;
        public SqlConnection GetConnection()
        {
            try
            {
                conn = new SqlConnection("PUT IN SQL STRING HERE");
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"warning: {ex}");
                return conn;
            }
        }

        public SqlDataReader GetReader(string query, params string[] sqlParams)
        {
            SqlCommand cmd = new SqlCommand(query);

            for (int i = 0; i < sqlParams.Length; i++)
            {
                cmd.Parameters.Add("@" + i, SqlDbType.VarChar);
                cmd.Parameters["@" + i].Value = sqlParams[i];
            }

            cmd.CommandType = CommandType.Text;
            cmd.Connection = this.GetConnection();
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataAdapter CreateSet(string query, params string[] sqlParams)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter();

            dataAdapter.SelectCommand = new SqlCommand(query);

            for (int i = 0; i < sqlParams.Length; i++)
            {
                dataAdapter.SelectCommand.Parameters.Add("@" + i, SqlDbType.VarChar);
                dataAdapter.SelectCommand.Parameters["@" + i].Value = sqlParams[i];
            }

            dataAdapter.SelectCommand.Connection = this.GetConnection();

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            return dataAdapter;
        }


        public void CloseConnection()
        {
            if (conn != null && conn.State == ConnectionState.Open)
            {
                this.conn.Close();
            }
        }
    }


}