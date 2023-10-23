using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CcCore.Base.DbManager
{
    public class DatabaseContext
    {
        private DatabaseConnectionData dbconnection;


        public DatabaseContext(DatabaseConnectionData dbdata)
        {
            dbconnection = dbdata;
        }





        protected List<Dictionary<string, string>> DoQuery(string query)
        {
            List<Dictionary<string, string>> resultset = new List<Dictionary<string, string>>();
            using (MySqlConnection mscon = new MySqlConnection(dbconnection.ToString()))
            using (MySqlCommand qperformer = new MySqlCommand(query, mscon))
            {
                mscon.Open();
                using (MySqlDataReader reader = qperformer.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int affectedFields = reader.FieldCount;
                        while (reader.Read())
                        {
                            Dictionary<string, string> tempres = new Dictionary<string, string>();
                            for (int i = 0; i < affectedFields; i++)
                            {
                                string fieldValue = "";
                                try
                                {
                                    fieldValue = reader.GetString(i);
                                }
                                catch
                                {
                                    fieldValue = null;
                                }
                                tempres.Add(reader.GetName(i), fieldValue);
                            }
                            resultset.Add(tempres);
                        }
                    }
                }
            }
            return resultset;
        }
    }

}
