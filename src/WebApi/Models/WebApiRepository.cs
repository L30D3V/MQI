namespace WebApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MongoDB.Driver;

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

        public List<ValuePairTest> ListValues()
        {
            try
            {
                List<ValuePairTest> values = new List<ValuePairTest>();
                values = _context.TestValues.Find(_ => true).ToList();

                return values;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista de valores", ex);
            }
        }

        public ValuePairTest GetValueById(string id)
        {
            try
            {
                ValuePairTest value = new ValuePairTest();
                value = _context.TestValues.Find(x => x.id.Equals(id)).FirstOrDefault();

                return value;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar valor", ex);
            }
        }

        // Recuperar valor específico por id
        public bool DeleteById(string id)
        {
            try
            {
                FilterDefinition<ValuePairTest> filter = Builders<ValuePairTest>.Filter.Eq(m => m.id, id);
                DeleteResult deleteResult = _context.TestValues.DeleteOne(filter);
                return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao deletar o valor.", ex);
            }
        }

        // Editar valor
        public bool EditById(ValuePairTest value)
        {
            try
            {
                ReplaceOneResult updateResult = _context.TestValues.ReplaceOne(filter: g => g.id == value.id, replacement: value);
                return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar o valor.", ex);
            }
        }
    }
}