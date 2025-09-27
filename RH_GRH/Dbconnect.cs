using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace RH_GRH
{
    internal class Dbconnect
    {
        MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=rhplusCshrp");
       // MySqlConnection connect = new MySqlConnection("server=localhost;port=3306;user=root;password=;database=rhplusCshrp;");

        public MySqlConnection getconnection
        {
            get
            {
                return connect;
            }
        }
        public void openConnect()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
                connect.Open();
        }

        public void closeConnect()
        {
         if(connect.State == System.Data.ConnectionState.Open)
                connect.Close();
        }



    }
}
