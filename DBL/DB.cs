using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DBL
{
    public abstract class DB
    {
        private const string MySqlConnSTR = $@"server=localhost;
                                    user id=root;";

        protected DbConnection conn;
        protected DbCommand cmd;
        protected DbDataReader reader;

        protected DB()
        {
            string pass = Environment.MachineName == new String("LIORSCHNAIDER") ? "1524" : "josh17rog";
            string newSqlConnSTR = MySqlConnSTR+ $"password={pass};";
            newSqlConnSTR += "persistsecurityinfo = True; database = vividdatabase";
            if (conn == null)
            {
                conn = new MySqlConnection(newSqlConnSTR);
            }
            cmd = new MySqlCommand();
            cmd.Connection = conn;
            reader = null;
        }
    }
}
