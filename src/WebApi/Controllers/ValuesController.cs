using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly Dictionary<string, string> initial_tests = new Dictionary<string, string>();

        // GET - Retorna todos os valores salvos em initial_tests
        [HttpGet]
        public IEnumerable<ValuePairTest> Get()
        {
            foreach(var test in initial_tests){
                yield return new ValuePairTest(test.Key, test.Value);
            }
        }

        // GET - Retorna valor com o id informado ou nulo caso inexistente
        [HttpGet("{id}")]
        public ValuePairTest Get(string id)
        {
            return initial_tests.ContainsKey(id) ? new ValuePairTest(id, initial_tests[id]) : null;
        }

        // POST - Insere novo valor com id aleatório
        [HttpPost]
        public void Post([FromBody] ValueTest valueEnvelope)
        {
            var id = Guid.NewGuid().ToString();
            initial_tests.Add(id, valueEnvelope.value);
        }

        // PUT - Insere valor no id informado
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] ValueTest valueEnvelope)
        {
            if (initial_tests.ContainsKey(id))
                initial_tests[id] = valueEnvelope.value;
        }

        // DELETE - Remove valor no id informado
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (initial_tests.ContainsKey(id))
                initial_tests.Remove(id);
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
