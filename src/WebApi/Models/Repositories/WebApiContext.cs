using MongoDB.Driver;
using MongoDB.Driver.GridFS;

namespace WebApi.Models
{

    public class WebApiContext: IWebApiContext
    {
        private readonly IMongoDatabase _db;
        public readonly GridFSBucket _gridFS;

        public WebApiContext(MongoDbConfiguration config)
        {
            var client = new MongoClient(config.ConnectionString);

            _db = client.GetDatabase(config.Database);
            _gridFS = new GridFSBucket(_db, new GridFSBucketOptions{ BucketName = "fotos_funcionarios" });
        }

        public IMongoCollection<ValuePairTest> TestValues => _db.GetCollection<ValuePairTest>("values");
        public IMongoCollection<Funcionario> Funcionarios => _db.GetCollection<Funcionario>("Funcionarios");
        public GridFSBucket GridFS => _gridFS;
    }
}