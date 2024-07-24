namespace Proximity_Alert{
    using System.Runtime.InteropServices;
    using Newtonsoft.Json;
    using System.Text;
    using System.IO;
    class Configuration_File_Management{
        public async Task<bool> Create_Configuration_File(string config_file){
            
            FileStream file_writer = File.OpenWrite(config_file);
            
            try{      
                string? serialised_file = await SerialiseConfig();      
                if(serialised_file != null){
                    byte[] binary_buffer = Encoding.UTF8.GetBytes(serialised_file);    
                    await file_writer.WriteAsync(binary_buffer, 0, binary_buffer.Length);
                    await file_writer.FlushAsync();   
                }    
            }
            catch{

            }
            finally{
                await file_writer.DisposeAsync();
            }

            return true;
        }

        public async Task<Configuration_File_Model?> Read_Configuration_File(string config_file){
            Configuration_File_Model? model = new Configuration_File_Model();

            FileStream file_reader = File.OpenRead(config_file);
            try{
                byte[] binary_buffer = new byte[file_reader.Length];
                await file_reader.ReadAsync(binary_buffer, 0, binary_buffer.Length);

                model = await DeserialiseConfig(Encoding.UTF8.GetString(binary_buffer));
            }
            catch{

            }
            finally{
                await file_reader.DisposeAsync();
            }

            return model;
        }

        private async Task<string?> SerialiseConfig(){
            StringBuilder? result = new StringBuilder();
            

            Configuration_File_Model model = new Configuration_File_Model(); 
            
            TextWriter tw = new StringWriter(result);

            try{
                JsonTextWriter jw = new JsonTextWriter(tw);
                JsonSerializer serialiser = new JsonSerializer(){
                    Formatting = Formatting.Indented
                }; 
                serialiser.Serialize(jw, model);

                await tw.FlushAsync();
            }
            catch{

            }
            finally{
                await tw.DisposeAsync();    
            }

            return result?.ToString();
        }

        private Task<Configuration_File_Model?> DeserialiseConfig(string serialised_config){

            Configuration_File_Model? model = new Configuration_File_Model(); 
            
            TextReader tr = new StringReader(serialised_config);

            try{
                JsonTextReader jr = new JsonTextReader(tr);
                JsonSerializer serialiser = new JsonSerializer(); 
                model = serialiser.Deserialize<Configuration_File_Model>(jr);
            }
            catch{

            }
            finally{
                tr.Dispose();    
            }

            return Task.FromResult(model);
        }
    }
}