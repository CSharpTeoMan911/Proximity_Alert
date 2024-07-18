
namespace Proximity_Alert
{
    using System.Runtime.InteropServices;
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

        public Task<ReturnType> Get<Value, ReturnType>(Value value)
        {
            StringBuilder builder = new StringBuilder(path);
            PathBuilder(builder, config_dir);

            if(Directory.Exists(builder.ToString()) == true) {
                PathBuilder(builder, file_name);
            }
            else{
                PathBuilder(builder, file_name);
                if(File.Exists(builder.ToString()) == true)
                {

                }
                else
                {

                }
            }         

            throw new NotImplementedException();
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
            if(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) == true){
                builder.Append("\\");
            }
            else{
                builder.Append("/");
            }
            builder.Append(section);
        }
    }
}