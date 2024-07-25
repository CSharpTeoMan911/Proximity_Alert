
namespace Proximity_Alert
{
    using Firebase.Auth;
    using Firebase.Auth.Providers;
    using Firebase.Auth.Repository;
    using Firebase.Storage;
    using Firebase.Database;
    using System.Text;
    class Firebase_CRUD : CRUD_Strategy
    {
        

        public async Task<ReturnType?> Delete<Value, ReturnType>(Value? value)
        {
            try{
                Configuration_File_Model? model = value as Configuration_File_Model;
                if(model != null){
                        FirebaseStorage? storage = await GetStorage(model);

                        DateTime current = DateTime.Now;

                        int start = model.proximity_alert_expiration_start;
                        int end = model.proximity_alert_expiration_end;

                        while(start <= end){
                            DateTime past = current.AddDays(start * -1);
                            if(storage != null)
                                await storage.Child(past.ToString("yyyyMMdd")).DeleteAsync();
                            start++;
                        }
                    }
                }
            catch(Exception E){
                Console.WriteLine($"\n\n{E.Message}\n\n");
            }


            return (ReturnType?)(object?)true;
        }

        public Task<ReturnType?> Get<Value, ReturnType>(Value? value)
        {   
            throw new NotImplementedException();
        }

        public async Task<ReturnType?> Insert<Value, ReturnType>(Value? value)
        {
            Tuple<FileStream, Configuration_File_Model>? buffer = value as Tuple<FileStream, Configuration_File_Model>;
            FileStream? binary_buffer = buffer?.Item1;
            Configuration_File_Model? model = buffer?.Item2;

            
            FirebaseStorage? storage = await GetStorage(model);
            FirebaseClient? database = await GetDatabase(model);

            string main_folder = DateTime.Now.ToString("yyyyMMdd");
            string proximity_alert = DateTime.Now.ToString("yyyyMMddmmss");

            StringBuilder builder = new StringBuilder(proximity_alert);
            builder.Append(".jpeg");

            string? final_path = builder.ToString();
            Firebase_Storage_File file = new Firebase_Storage_File();
            file.file_name = final_path;

            if(storage != null)
                await storage.Child(main_folder).Child(final_path).PutAsync(binary_buffer);

            if(database != null)
                await database.Child("Files/").PostAsync("Test", false);
            

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

        private async Task<FirebaseStorage?> GetStorage(Configuration_File_Model? model){
            string IdToken = await Authenticate(model);

            FirebaseStorage storage = new FirebaseStorage(
            model?.firebase_storage_bucket_url,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(IdToken),
                ThrowOnCancel = true,
            });

            return storage;
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
    }
}