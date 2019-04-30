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
            List<ValuePairTest> listValues = DbContext.TestValues.Find(m => true).ToList();

            return View(listValues);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ValuePairTest value)
        {
            value.id = Guid.NewGuid().ToString();

            DbContext.TestValues.InsertOne(value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            var value = DbContext.TestValues.Find(x => x.id == id).FirstOrDefault();
            return View(value);
        }

        [HttpPost]
        public IActionResult Update(ValuePairTest value)
        {
            DbContext.TestValues.ReplaceOne(x => x.id == value.id, value);

            return View(value);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            DbContext.TestValues.DeleteOne(x => x.id == id);

            return RedirectToAction("Index");
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
