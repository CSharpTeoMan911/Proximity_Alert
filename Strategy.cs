
namespace Proximity_Alert
{
    class Strategy:CRUD_Strategy{

        private CRUD_Strategy crud;
        public Strategy(CRUD_Strategy _crud){
            crud = _crud;
        }

        public async Task<ReturnType?> Delete<Value, ReturnType>(Value? value)
        {
            return await crud.Delete<Value, ReturnType>(value);
        }

        public async Task<ReturnType?> Get<Value, ReturnType>(Value? value)
        {
            return await crud.Get<Value, ReturnType>(value);
        }

        public async Task<ReturnType?> Insert<Value, ReturnType>(Value? value)
        {
            return await crud.Insert<Value, ReturnType>(value);
        }

        public async Task<ReturnType?> Update<Value, ReturnType>(Value? value)
        {
            return await crud.Update<Value, ReturnType>(value);
        }
    }
}