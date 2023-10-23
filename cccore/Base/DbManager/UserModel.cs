using System;

namespace CcCore.Base.DbManager
{
    [TableName("usermodel_table")]
    public class UserModel: DatabaseModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        protected override DatabaseContext Context { get; set; }



        public UserModel(DatabaseContext context) => Context = context;


        public UserModel() { }


        public override DatabaseContext GetContext()
        {
            return Context;
        }
    }
}
