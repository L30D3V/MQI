using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebInterface.Context;
using WebInterface.Models;

namespace WebInterface.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext DbContext = new ModelContext();

        public IActionResult Index()
        {
            try
            {
                List<ValuePairTest> listValues = DbContext.TestValues.Find(m => true).ToList();
                return View(listValues);
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] ValuePairTest value)
        {
            try
            {
                ValuePairTest newData = new ValuePairTest
                {
                    id = Guid.NewGuid().ToString(),
                    value = value.value
                };

                DbContext.TestValues.InsertOne(newData);

                TempData["Success"] = "Dados salvos com sucesso. Id: " + newData.id + " Value: " + newData.value;
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Falha ao salvar dados. Descrição do erro: " + ex;
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var value = DbContext.TestValues.Find(x => x.id == id).FirstOrDefault();
            return View(value);
        }

        [HttpPost]
        public IActionResult Update([FromForm] ValuePairTest value)
        {
            try
            {
                ValuePairTest newData = new ValuePairTest
                {
                    id = value.id,
                    value = value.value
                };

                DbContext.TestValues.ReplaceOne(x => x.id == value.id, newData);

                return View(newData);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Erro ao atualizar o valor. Descrição do erro: " + ex.Message;
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            DbContext.TestValues.DeleteOne(x => x.id == id);

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
