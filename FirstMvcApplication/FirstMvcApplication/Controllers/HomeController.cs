using FirstMvcApplication.Models;
using FirstMvcApplication.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FirstMvcApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataService _dataService;
        //konstruktorius kiekviena karta ieina kai darom nauja call
        public HomeController(ILogger<HomeController> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("NamesList");
        }

        public IActionResult AboutMe()
        {
            PersonModel person = new PersonModel() { Name = "Giedre" };
            return View();
        }
        //display page with the form
        public IActionResult SendSubmitData(PersonModel model)
        {

            // System.IO.File.WriteAllText("text.txt",model.Name);
            _dataService.Add(model);
            return RedirectToAction("DisplaySubmitData");
        }
        //will received filled model and will save to file
        public IActionResult DisplaySubmitData()
        {
            var emptyModel = new PersonModel();
            return View(emptyModel);
        }

        public IActionResult NamesList()
        {
            var persons = _dataService.GetAll();
            var personsList = new NamesListModel()
            {
                Persons = persons
            };
            return View(personsList);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
