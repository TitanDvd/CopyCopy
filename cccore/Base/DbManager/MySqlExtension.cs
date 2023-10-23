using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CcCore.Base.DbManager
{
    public class QueryBuilder
    {
        public static string Query { get; set; }


        public QueryBuilder(string query)
        {
            Query += query;
        }
    }


    public static class MySqlExtension
    {
        private static string queryBuilder = "";

        public static List<T> Select<T, TResult>(this T model, Func<T, TResult> pred, string table = null) where T : DatabaseModel
        {
            string selectStatement = "SELECT ";
            var props = pred.Method.ReturnType.GetRuntimeProperties();

            var attr = (TableNameAttribute)typeof(T).GetCustomAttribute(typeof(TableNameAttribute));
            
            int c = props.Count();
            for (int i = 0; i < c; i++)
                if (c == i + 1)
                    selectStatement += $"{props.ElementAt(i).Name}";
                else
                    selectStatement += $"{props.ElementAt(i).Name},";

        
            if (attr is null)
            {
                if (table == null)
                    table = typeof(T).Name.ToLower();
            }
            else
                table = attr.TableName;
           
           

            string query = $"{selectStatement} FROM {table}";
            var iDbModel = typeof(T).BaseType.Name;

            if(iDbModel == "DatabaseModel")
            {
                var context = model.GetContext();
                var mtype = context.GetType();
                var metthod = mtype.GetMethod("DoQuery", BindingFlags.Instance | BindingFlags.NonPublic);
                List<Dictionary<string, string>> resultset = (List<Dictionary<string, string>>)metthod.Invoke(context, new object[] { query });
                List<T> n = new List<T>();
                foreach (var r in resultset)
                    n.Add(MapTo<T>(r));

                return n;
            }
           

            return new List<T>();
        }




        public static QueryBuilder SelectAll<T>(this T model, string table = null) where T : DatabaseModel
        {
            string selectStatement = "SELECT *";
            var attr = (TableNameAttribute)typeof(T).GetCustomAttribute(typeof(TableNameAttribute));
            if (attr is null)
            {
                if (table == null)
                    table = typeof(T).Name.ToLower();
            }
            else
                table = attr.TableName;



          return new QueryBuilder($"{selectStatement} FROM {table}");



            //var iDbModel = typeof(T).BaseType.Name;

            //if (iDbModel == "DatabaseModel")
            //{
            //    var context = model.GetContext();
            //    var mtype = context.GetType();
            //    var metthod = mtype.GetMethod("DoQuery", BindingFlags.Instance | BindingFlags.NonPublic);
            //    List<Dictionary<string, string>> resultset = (List<Dictionary<string, string>>)metthod.Invoke(context, new object[] { query });
            //    List<T> n = new List<T>();
            //    foreach (var r in resultset)
            //        n.Add(MapTo<T>(r));

            //    return n;
            //}


            //return new List<T>();
        }



        public static QueryBuilder JoinTo<T>(this QueryBuilder queryBuilder, string joinDirection = "LEFT", string table = null) where T : DatabaseModel
        {
            return new QueryBuilder($" {joinDirection} JOIN {typeof(T).Name}");
        }


        private static T MapTo<T>(Dictionary<string, string> rsets)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));

            foreach (var rset in rsets)
            {

                var iType = instance.GetType();
                try
                {
                    var prop = iType.GetProperty(rset.Key);
                    prop.SetValue(instance, Convert.ChangeType(rset.Value, prop.PropertyType));
                }
                catch { }
            }

            return instance;
        }
    }
}
