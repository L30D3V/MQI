using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Serilog;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // Inicializa variáveis globais
        private readonly ILogger logger = Log.ForContext<ValuesController>();
        private readonly MongoClient mongoClient;
        private readonly IMongoDatabase db;
        
        //Construtor ValuesController
        public ValuesController() {
            logger.Verbose("ValuesController criado");

            // Configura MongoDB e conecta-se ao banco de dados
            var mongoClientSettings = new MongoClientSettings();
            mongoClient = new MongoClient(mongoClientSettings);
            db = mongoClient.GetDatabase(MongoDbConfiguration.DatabaseName);

            // Verifica se Collection "values" existe e cria uma caso negativo
            var c = db.GetCollection<ValuePairTest>("values");
            if (c == null)
            {
                db.CreateCollection("values");
            }
        }

        // GET - Retorna todos os valores salvos em initial_tests
        [HttpGet]
        public IEnumerable<ValuePairTest> Get()
        {
            try {
                // Log GET
                logger.Information("GET /api/values");

                // Recupera todos os valores em "values"
                var c = db.GetCollection<ValuePairTest>("values");
                // Retorna todos os valores em formato de Lista
                return c.Find(Builders<ValuePairTest>.Filter.Empty).ToList();
            }
            catch (Exception ex) {
                logger.Information("Falha ao processar comando: " + ex.Message);
                return null;
            }
        }

        // GET - Retorna valor com o id informado ou nulo caso inexistente
        [HttpGet("{id}")]
        public ValuePairTest Get(string id)
        {
            try {
                // Log GET por ID
                logger.Information($"GET /api/values/{id}");

                // Recupera todos os valores em "values"
                var c = db.GetCollection<ValuePairTest>("values");
                // Retorna valores que correspondem ao ID informado ou nulo caso não exista
                return c.Find(Builders<ValuePairTest>.Filter.Eq(x => x.id, id)).FirstOrDefault();
            }
            catch (Exception ex) {
                logger.Information("Falha ao processar comando: " + ex.Message);
                return null;
            }
        }

        // POST - Insere novo valor com id aleatório
        [HttpPost]
        public void Post([FromBody] ValueTest valueEnvelope)
        {
            try {
                // Extrai value informado
                var v = valueEnvelope?.value;
                // Log POST
                logger.Information($"POST /api/values {v}");
                // Cria novo ID
                var id = Guid.NewGuid().ToString();
                // Recupera todos os valores em "values"
                var c = db.GetCollection<ValuePairTest>("values");
                // Cria novo PairType de dados
                var d = new ValuePairTest(id, v);
                // Insere valor criado no database
                c.InsertOne(d);
            }
            catch (Exception ex) {
                logger.Information("Falha ao processar comando: " + ex.Message);
            }
        }

        // PUT - Insere valor no id informado
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ValueTest valueEnvelope)
        {
            try {
                // Extrai value informado
                var v = valueEnvelope?.value;
                // Log PUT
                logger.Information($"PUT /api/values {v}");
                // Recupera todos os valores em "values"
                var c = db.GetCollection<ValuePairTest>("values");
                // Filtra valores por id e faz update de seu value
                c.UpdateOne(Builders<ValuePairTest>.Filter.Eq(x => x.id, id), Builders<ValuePairTest>.Update.Set(x => x.value, v));
            }
            catch (Exception ex) {
                logger.Information("Falha ao processar comando: " + ex.Message);
            }
        }

        // DELETE - Remove valor no id informado
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            try {
                // Log DELETE
                logger.Information($"DELETE /api/values/{id}");

                // Recupera todos os valores em "values"
                var c = db.GetCollection<ValuePairTest>("values");
                // Filtra valor por ID e remove valor correspondente
                c.DeleteOne(Builders<ValuePairTest>.Filter.Eq(x => x.id, id));
            }
            catch (Exception ex) {
                logger.Information("Falha ao processar comando: " + ex.Message);
            }
        }


        // Valor sem Id
        public class ValueTest 
        {
            public string value { get; private set; }

            public ValueTest(string value) {
                this.value = value;
            }
        }

        // Classe salva no Dictionary initial_tests
        public class ValuePairTest
        {
            public string id { get; private set; }
            public string value { get; private set; }

            public ValuePairTest(string id, string value) {
                this.id = id;
                this.value = value;
            }
        }
    }
}
