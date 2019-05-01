using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IWebApiRepository _repo;
        private readonly ILogger logger = Log.ForContext<ValuesController>();

        public ValuesController(IWebApiRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValuePairTest>>> Get()
        {
            try {
                logger.Information("GET /api/values");
                return new ObjectResult(await _repo.GetAllValues());
            } catch (Exception ex) {
                logger.Information("FALHA AO EXECUTAR A CHAMADA DA API: " + ex.Message);
                return new NoContentResult();
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ValuePairTest>> Get(string id)
        {
            try {
                logger.Information($"GET /api/values/{id}");
                var value = await _repo.GetValue(id);

                if (value == null)
                    return new NotFoundResult();
                
                return new ObjectResult(value);
            } catch (Exception ex) {
                logger.Information("FALHA AO EXECUTAR A CHAMADA DA API: " + ex.Message);
                return new NoContentResult();
            }
            
        }

        [HttpPost]
        public async Task<ActionResult<ValuePairTest>> Post([FromBody] ValuePairTest value)
        {
            try {
                logger.Information($"POST /api/values {value}");
                value.id = Guid.NewGuid().ToString();
                await _repo.Create(value);
                return new OkObjectResult(value);
            } catch (Exception ex) {
                logger.Information("FALHA AO EXECUTAR A CHAMADA DA API: " + ex.Message);
                return new NoContentResult();
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ValuePairTest>> Put(string id, [FromBody] ValuePairTest value)
        {
            try {
                logger.Information($"PUT /api/values/{value}");
                var valueBD = await _repo.GetValue(id);

                if (valueBD == null)
                    return new NotFoundResult();

                await _repo.Update(value);

                return new OkObjectResult(value);            
            } catch (Exception ex) {
                logger.Information("FALHA AO EXECUTAR A CHAMADA DA API: " + ex.Message);
                return new NoContentResult();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try {
                logger.Information($"DELETE /api/values/{id}");
                var value = await _repo.GetValue(id);

                if (value == null)
                    return new NotFoundResult();

                await _repo.Delete(id);

                return new OkResult();
            } catch (Exception ex) {
                logger.Information("FALHA AO EXECUTAR A CHAMADA DA API: " + ex.Message);
                return new NoContentResult();
            }
        }
    }
}
