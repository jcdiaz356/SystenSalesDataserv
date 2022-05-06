using System;
using System.Collections.Generic;

using MySql.Data.MySqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    class ConnectionMysql

    {

        private string Base;
        private string Server;
        private string User;
        private string Password;
        private bool Security;
        private static ConnectionMysql  Con= null;

        private ConnectionMysql()
        {
           

            this.Base = "";
            this.Server = "";
            this.User = "";
            this.Password = "";
            this.Security = true;
           
        }


        public MySqlConnection CrearConexion()
        {
            MySqlConnection Cadena = new MySqlConnection();
            try
            {
               // myConnectionString = "server=127.0.0.1;uid=root;" +    "pwd=12345;database=test";

                Cadena.ConnectionString = "Server=" + this.Server + ";uid=" + this.User + ";pwd=" + this.Password + ";Database=" + this.Base + ";";
                //if (this.Security)
                //{
                //    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                //}
                //else
                //{
                //    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.User + ";Password=" + this.Password;
                //}
            }
            catch (MySqlException ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        public static ConnectionMysql getInstancia()
        {
            if (Con == null)
            {
                Con = new ConnectionMysql();
            }
            return Con;
        }
    }
}
