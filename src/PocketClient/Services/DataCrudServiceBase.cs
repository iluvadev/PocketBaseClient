namespace PocketClient.Services
{
    public abstract class DataCrudServiceBase : ServiceBase
    {
        protected DataServiceBase Data { get; }
        public DataCrudServiceBase(DataServiceBase dataService) : base(dataService.App)
        {
            Data = dataService;
        }

        /*
         * METHODS:
         *      ById<T>(string id)
         *      SaveAsync<T>(T item)
         *      DeleteAsync<T>(T item)
         *      
         *      Reload<T>(T item)
         *      
         *      Discard<T>(T item)
         *      Discard<T>()
         *      Discard()
        */
    }
}
