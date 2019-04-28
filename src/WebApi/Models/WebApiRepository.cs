namespace WebApi.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;
    using MongoDB.Bson;
    using System.Linq;

    public class WebApiRepository : IWebApiRepository
    {
        private readonly IWebApiContext _context;

        public WebApiRepository(IWebApiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ValuePairTest>> GetAllValues()
        {
            return await _context.TestValues.Find(_ => true).ToListAsync();
        }
        public Task<ValuePairTest> GetValue(string id)
        {
            FilterDefinition<ValuePairTest> filter = Builders<ValuePairTest>.Filter.Eq(m => m.id, id);
            return _context.TestValues.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Create(ValuePairTest value)
        {
            await _context.TestValues.InsertOneAsync(value);
        }
        public async Task<bool> Update(ValuePairTest value)
        {
            ReplaceOneResult updateResult = await _context.TestValues.ReplaceOneAsync(filter: g => g.id == value.id, replacement: value);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string id)
        {
            FilterDefinition<ValuePairTest> filter = Builders<ValuePairTest>.Filter.Eq(m => m.id, id);
            DeleteResult deleteResult = await _context.TestValues.DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}