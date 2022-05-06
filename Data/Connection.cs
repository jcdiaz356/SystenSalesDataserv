using System;

using System.Data.SqlClient;


namespace Data
{
    class Connection
    {

        private string Base;
        private string Server;
        private string User;
        private string Password;
        private bool Security;
        private static Connection Con = null;

        private Connection()
        {
            this.Base = "DBVENTASDataserv";
            this.Server = "DESKTOP-FN8OGML\\SQLEXPRESS";
            this.User = "sa";
            this.Password = "irmaguevara";
            this.Security = true;
        }
        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Server + "; Database=" + this.Base + ";";
                if (this.Security)
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.User + ";Password=" + this.Password;
                }
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;
            }
            return Cadena;
        }

        public static Connection getInstancia()
        {
            if (Con == null)
            {
                Con = new Connection();
            }
            return Con;
        }
    }
}
