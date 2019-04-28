namespace WebApi.Models
{
    using WebApi;
    using MongoDB.Driver;

    public class WebApiContext: IWebApiContext
    {
        private readonly IMongoDatabase _db;

        public WebApiContext(MongoDbConfiguration config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }

        public IMongoCollection<ValuePairTest> TestValues => _db.GetCollection<ValuePairTest>("values");
    }
}