using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Collections;
using System.Linq.Expressions;

namespace CcCore.Base.DbManager
{
    public class DatabaseConnectionData
    {
        public string Username { get; set; }
        public string DatabaseName { get; set; }
        public string DatabasePassword { get; set; }
        public string DatabaseHost { get; set; }
        public int DatabasePort { get; set; }


        public override string ToString()
        {
            return 
                $"datasource =\"{DatabaseHost}\";" +
                $"port       =\"{DatabasePort}\";" +
                $"username   =\"{Username}\";" +
                $"password   =\"{DatabasePassword}\";" +
                $"database   =\"{DatabaseName}\";" +
                $"default command timeout=0;";
        }
    }
}
