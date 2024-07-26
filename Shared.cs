namespace Proximity_Alert{
    using System.Text;
    using System.IO;
    class Shared{
        protected static readonly string path = System.AppDomain.CurrentDomain.BaseDirectory;  
        private static readonly Strategy firebase_crud = new Strategy(new Firebase_CRUD());
        private static readonly Strategy config_crud = new Strategy(new Configuration_CRUD());
        protected static DateTime last_proximity_alert = DateTime.Now.AddMinutes(-10);
        protected static Configuration_File_Model? model = new Configuration_File_Model();

        protected static async Task<bool> Get_Config(){
            model = await config_crud.Get<object?, Configuration_File_Model>(null);
            return true;
        }

        protected static async Task<bool> Insert_Alert(){
            await firebase_crud.Insert<Configuration_File_Model?, bool>(model);
            return true;
        }

        protected static async Task<bool> Delete_Alerts(){
            await firebase_crud.Delete<Configuration_File_Model?, object?>(model);
            return true;
        }
    }
}