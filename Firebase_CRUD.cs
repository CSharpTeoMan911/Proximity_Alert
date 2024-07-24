
namespace Proximity_Alert
{
    using Firebase.Auth;
    using Firebase.Auth.Providers;
    using Firebase.Auth.Repository;
    using Firebase.Storage;
    using System.Text;
    class Firebase_CRUD : CRUD_Strategy
    {
        

        public Task<ReturnType?> Delete<Value, ReturnType>(Value? value)
        {
            throw new NotImplementedException();
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

            
            FirebaseStorage? storage = await GetStorage(model, binary_buffer);

            if(storage != null){
                string main_folder = DateTime.Now.ToString("yyyyMMdd");
                string proximity_alert = DateTime.Now.ToString("yyyyMMddmmss");

                StringBuilder builder = new StringBuilder(proximity_alert);
                builder.Append(".jpeg");

                await storage.Child(main_folder).Child(builder.ToString()).PutAsync(binary_buffer);
            }

            

            return (ReturnType)(object) true;
        }

        public Task<ReturnType?> Update<Value, ReturnType>(Value? value)
        {
            throw new NotImplementedException();
        }

        private async Task<FirebaseStorage?> GetStorage(Configuration_File_Model? model, FileStream? binary_buffer){
            FirebaseAuthClient client = new FirebaseAuthClient(new FirebaseAuthConfig(){
                ApiKey = model?.api_key,
                AuthDomain = "proximity-alert-raspberrypi.firebaseapp.com",
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            });
            UserCredential credentials = await client.SignInWithEmailAndPasswordAsync(model?.user_email, model?.user_password);

            FirebaseStorage storage = new FirebaseStorage(
            model?.firebase_storage_bucket_url,
            new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(credentials.User.Credential.IdToken),
                ThrowOnCancel = true,
            });

            return storage;
        }
    }
}