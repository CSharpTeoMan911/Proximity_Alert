namespace Proximity_Alert{
    using System.Text;
    using System.IO;
    class Shared{
        public static readonly string path = Environment.CurrentDirectory;
        public static readonly string config_dir = "conf";
        public static readonly string file_name = "config.json";

        private static Strategy? firebase_crud;
        private static readonly Strategy config_crud = new Strategy(new Configuration_CRUD());
        protected static DateTime last_proximity_alert = DateTime.Now.AddMinutes(-10);
        protected static Configuration_File_Model? model = new Configuration_File_Model();
    

        protected static async Task Get_Config(){
            model = await config_crud.Get<object?, Configuration_File_Model>(null);
            firebase_crud = new Strategy(new Firebase_CRUD(model));
        }

        protected static async Task Insert_Alert() => await firebase_crud.Insert<Configuration_File_Model?, bool>(model);

        protected static async Task Delete_Alerts() => await firebase_crud.Delete<Configuration_File_Model?, object?>(model);
    }
}