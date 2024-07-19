
namespace Proximity_Alert
{
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System.Text;
    using System.IO;
    class Configuration_CRUD : Shared, CRUD_Strategy
    {
        private readonly Configuration_File_Management file_management = new Configuration_File_Management();
        string config_dir = "conf";
        string file_name = "config.json";
        public Task<ReturnType> Delete<Value, ReturnType>(Value value)
        {
            throw new NotImplementedException();
        }

        public async Task<ReturnType> Get<Value, ReturnType>(Value value)
        {
            Configuration_File_Model model = new Configuration_File_Model();

            StringBuilder builder = new StringBuilder(path);
            PathBuilder(builder, config_dir);

            if(Directory.Exists(builder.ToString()) == true) {
                PathBuilder(builder, file_name);
                if(File.Exists(builder.ToString()) == true)
                {
                    await file_management.Read_Configuration_File(builder.ToString());
                }
                else
                {
                    await file_management.Create_Configuration_File(builder.ToString());
                }
            }
            else{
                Directory.CreateDirectory(builder.ToString());
                PathBuilder(builder, file_name);
                await file_management.Create_Configuration_File(builder.ToString());
            }         

            return (ReturnType)(object)model;
        }

        public Task<ReturnType> Insert<Value, ReturnType>(Value value)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnType> Update<Value, ReturnType>(Value value)
        {
            throw new NotImplementedException();
        }

        private void PathBuilder(StringBuilder builder, string section){
            builder.Append("/");
            builder.Append(section);
        }
    }
}