namespace Proximity_Alert{
    using System.Text;
    class Shared{
        protected static readonly string path = System.AppDomain.CurrentDomain.BaseDirectory;  
        protected static readonly Strategy firebase_crud = new Strategy(new Firebase_CRUD());
        protected static readonly Strategy config_crud = new Strategy(new Configuration_CRUD());
    }
}