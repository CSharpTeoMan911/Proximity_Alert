
namespace Proximity_Alert
{
    using Firebase.Auth;
    using Firebase.Auth.Providers;
    using Firebase.Auth.Repository;
    using Firebase.Storage;
    class Firebase_CRUD : CRUD_Strategy
    {
        

        public Task<ReturnType> Delete<Value, ReturnType>(Value value)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnType> Get<Value, ReturnType>(Value value)
        {
            
            throw new NotImplementedException();
        }

        public async Task<ReturnType> Insert<Value, ReturnType>(Value value)
        {
            Tuple<byte[], Configuration_File_Model>? buffer = value as Tuple<byte[], Configuration_File_Model>;
            byte[]? binary_buffer = buffer?.Item1;
            Configuration_File_Model? model = buffer?.Item2;

            //authentication
            FirebaseAuthClient client = new FirebaseAuthClient(new FirebaseAuthConfig(){
                ApiKey = model?.api_key
            });
            await client.SignInWithEmailAndPasswordAsync(model?.user_email, model?.user_password);

            

            throw new NotImplementedException();
        }

        public Task<ReturnType> Update<Value, ReturnType>(Value value)
        {
            throw new NotImplementedException();
        }
    }
}