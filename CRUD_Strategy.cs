namespace Proximity_Alert
{
    interface CRUD_Strategy
    {
        public Task<string> Get<Value>(Value value);
        public Task<bool> Insert<Value>(Value value);
        public Task<bool> Delete<Value>(Value value);
        public Task<bool> Update<Value>(Value value);
    }
}