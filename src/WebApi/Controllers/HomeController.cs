using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebApiRepository _repo;
        private readonly ILogger logger = Log.ForContext<ValuesController>();

        public HomeController(IWebApiRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<ValuePairTest> values = new List<ValuePairTest>();
            //var values = _repo.GetAllValues();

            return View(values);
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(string id)
        {
            ValuePairTest value = new ValuePairTest();
            //value = _repo.GetValueById(id);

            return View(value);
        }

        [HttpPost]
        public IActionResult Edit(ValuePairTest value)
        {
            try
            {
                bool editValue = _repo.EditById(value);
                if (editValue)
                {
                    TempData["Success"] = "Valor editado com sucesso.";
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Falha ao conectar com o banco.");
                }
            }
            catch(Exception ex)
            {
                TempData["Error"] = "Ocorreu um erro ao editar o valor. Falha: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            ValuePairTest value = new ValuePairTest();
            bool deleteValue = _repo.DeleteById(id);
            if (deleteValue)
            {
                TempData["Success"] = "Valor removido com sucesso.";
            }
            else
            {
                TempData["Error"] = "Ocorreu um erro ao remover o valor. Tente novamente.";
            }
            return RedirectToAction("Index");
        }
    }
}