namespace CcCore.Base.DbManager
{
    public abstract class DatabaseModel {
        virtual protected DatabaseContext Context { get; set; }
        public abstract DatabaseContext GetContext();
    }
}
