namespace Proximity_Alert
{
    interface CRUD_Strategy
    {
        public Task<ReturnType> Get<Value, ReturnType>(Value value);
        public Task<ReturnType> Insert<Value, ReturnType>(Value value);
        public Task<ReturnType> Delete<Value, ReturnType>(Value value);
        public Task<ReturnType> Update<Value, ReturnType>(Value value);
    }
}