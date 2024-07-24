namespace Proximity_Alert{
    using System.Text;
    using System.IO;
    class Shared{
        protected static readonly string path = System.AppDomain.CurrentDomain.BaseDirectory;  
        protected static readonly Strategy firebase_crud = new Strategy(new Firebase_CRUD());
        private static readonly Strategy config_crud = new Strategy(new Configuration_CRUD());
        protected static DateTime last_proximity_alert = DateTime.Now.AddMinutes(-10);
        protected static Configuration_File_Model? model = new Configuration_File_Model();

        protected static async Task<bool> Get_Config(){
            model = await config_crud.Get<object?, Configuration_File_Model>(null);
            return true;
        }

        protected static async Task<bool> Insert_Alert(string path){
            FileStream stream = File.Open(path, FileMode.Open);
            try{
                Tuple<FileStream, Configuration_File_Model?> payload = new Tuple<FileStream, Configuration_File_Model?>(stream, model); 

                await firebase_crud.Insert<Tuple<FileStream, Configuration_File_Model?>, bool>(payload);
            }
            catch(Exception E){
                Console.WriteLine(E.Message);
            }
            finally{
                await stream.DisposeAsync();
            }

            return true;
        }
    }
}