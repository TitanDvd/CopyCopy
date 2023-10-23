using System;

namespace CcCore.Base.DbManager
{
    public class TableNameAttribute : Attribute
    {
        public string TableName { get; set; }

        public TableNameAttribute(string tablename)
        {
            TableName = tablename;
        }
    }
}
