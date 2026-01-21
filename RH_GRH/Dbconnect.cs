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
        //MySqlConnection connect = new MySqlConnection("datasource=localhost;port=3306;username=root;password=;database=rhplusCshrp");
        MySqlConnection connect = new MySqlConnection("datasource=72.62.190.57;port=3306;username=portail_user;password=Root@508050rh;database=rhplusCshrp");

        //  MySqlConnection connect = new MySqlConnection("datasource=srv1909.hstgr.io;port=3306;username=u694924489_csharprhplus;password=Root@508050;database=u694924489_csharprhplus");

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
