using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebInterface.Models;

namespace WebInterface.Context
{
    public class ModelContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }
        public static string User { get;set; }
        public static string Password { get; set; }
        private IMongoDatabase _db { get; }

        public ModelContext()
        {
            try
            {
                MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));

                var mongoClient = new MongoClient(settings);
                _db = mongoClient.GetDatabase(DatabaseName);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível comunicar com o servidor", ex);
            }
        }

        public IMongoCollection<ValuePairTest> TestValues => _db.GetCollection<ValuePairTest>("values");
    }
}
