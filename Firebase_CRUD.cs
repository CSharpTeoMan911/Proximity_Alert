
namespace Proximity_Alert
{
    using Firebase.Auth;
    using Firebase.Auth.Providers;
    using Firebase.Auth.Repository;
    using Firebase.Database;
    using System.Text;
    using System.IO;
    using Newtonsoft.Json;

    class Firebase_CRUD : CRUD_Strategy
    {
        

        public async Task<ReturnType?> Delete<Value, ReturnType>(Value? value)
        {
            try{
                Configuration_File_Model? model = value as Configuration_File_Model;

                if(model != null){
                    FirebaseClient? database = await GetDatabase(model);
                
                    if(database != null){
                        string payload = await database.Child("Alerts").OnceAsJsonAsync();
                        Dictionary<string, Firebase_Database_Model>? deserialsed_payload = await DeserialisePayload(payload);

                        if(deserialsed_payload != null)
                        {
                            StringBuilder path_builder = new StringBuilder();
                            foreach(string s in deserialsed_payload.Keys){
                                int expiration = Convert.ToInt32(DateTime.Now.AddDays(-1 * model.proximity_alert_expiration_start).ToString("yyyyMMdd"));
                                int key = Convert.ToInt32(s);

                                if(key <= expiration)
                                {
                                    path_builder.Append("Alerts/");
                                    path_builder.Append(key);
                                    await database.Child(path_builder.ToString()).DeleteAsync();
                                    path_builder.Clear();
                                }
                            }
                        }
                    }
                }
            }
            catch { }


            return (ReturnType?)(object?)true;
        }

        public Task<ReturnType?> Get<Value, ReturnType>(Value? value)
        {   
            throw new NotImplementedException();
        }

        public async Task<ReturnType?> Insert<Value, ReturnType>(Value? value)
        {
            Configuration_File_Model? model = value as Configuration_File_Model;

            FirebaseClient? database = await GetDatabase(model);

            string main_folder = DateTime.Now.ToString("yyyyMMdd");
            string proximity_alert = DateTime.Now.ToString("yyyyMMddHHmmss");

            StringBuilder path_builder = new StringBuilder("Alerts/");
            path_builder.Append(main_folder);

            Firebase_Alert alert = new Firebase_Alert();
            alert.alert_name = proximity_alert;

            if(database != null)
                await database.Child(path_builder.ToString()).PostAsync(await SerialisePayload(alert), false);
            

            return (ReturnType)(object) true;
        }

        public Task<ReturnType?> Update<Value, ReturnType>(Value? value)
        {
            throw new NotImplementedException();
        }

        private async Task<string> Authenticate(Configuration_File_Model? model){
            string IdToken = String.Empty;
            FirebaseAuthClient client = new FirebaseAuthClient(new FirebaseAuthConfig(){
                ApiKey = model?.api_key,
                AuthDomain = model?.firebase_auth_domain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            });
            UserCredential credentials = await client.SignInWithEmailAndPasswordAsync(model?.user_email, model?.user_password);
            IdToken = credentials.User.Credential.IdToken;
            return IdToken;
        }


        private Task<FirebaseClient?> GetDatabase(Configuration_File_Model? model){
            FirebaseClient? database = new FirebaseClient(
            model?.firebase_database_url,
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Authenticate(model)
            });

            return Task.FromResult<FirebaseClient?>(database);
        }

        private async Task<string?> SerialisePayload(Firebase_Alert alert){
            StringBuilder? result = new StringBuilder();
            
            TextWriter tw = new StringWriter(result);

            try{
                JsonTextWriter jw = new JsonTextWriter(tw);
                JsonSerializer serialiser = new JsonSerializer(){
                    Formatting = Formatting.Indented
                }; 
                serialiser.Serialize(jw, alert);

                await tw.FlushAsync();
            }
            catch{

            }
            finally{
                await tw.DisposeAsync();    
            }

            return result?.ToString();
        }

        private Task<Dictionary<string, Firebase_Database_Model>?> DeserialisePayload(string serialised_alert){

            Dictionary<string, Firebase_Database_Model>? model = new Dictionary<string, Firebase_Database_Model>(); 
            
            TextReader tr = new StringReader(serialised_alert);

            try{
                JsonTextReader jr = new JsonTextReader(tr);
                JsonSerializer serialiser = new JsonSerializer(); 
                model = serialiser.Deserialize<Dictionary<string, Firebase_Database_Model>>(jr);
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