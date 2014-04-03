using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CasparCGPlayout.Database
{
    class DatabaseConnectionUtils
    {
        /// <summary>
        /// Generates a connection string for lazy bastards
        /// </summary>
        /// <param name="server">The name or IP of the machine where the MySQL server is running</param>
        /// <param name="databaseName">The name of the database (catalog)</param>
        /// <param name="user">The user id - root if there are no new users which have been created</param>
        /// <param name="pass">The user's password</param>
        /// <param name="timeout">Connection Timeout</param>
        /// <returns></returns>
        public static string CreateConnStr(string server, string databaseName, string user, string pass, string timeout)
        {
            //build the connection string
            string connStr = "server=" + server + ";database=" + databaseName + ";uid=" +
                user + ";password=" + pass + ";ConnectionTimeout="+timeout+";";

            //return the connection string
            return connStr;
        }

    }
}
