using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoListWebApp.Models;
using TodoListWebApp.Services;

namespace TodoListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataService _dataService;
        public HomeController(ILogger<HomeController> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("TodoItemsList");
        }

        public IActionResult AddNewTodo()
        {
            return View();
        }

        public IActionResult SendSubmitData(TodoModel model)
        {
            if (model.Name is not null)
            {
                _dataService.Add(model);
            }
            return RedirectToAction("AddNewTodo");
        }

        public IActionResult TodoItemsList()
        {
            var todos = _dataService.GetAll();
            var todosList = new TodoListModel()
            {
                Todos = todos
            };
            return View(todosList);
        }

        public IActionResult AddNewTodoToFile()
        {
            return View();
        }

        public IActionResult SendSubmitDataToFile(TodoModel model)
        {
            if (model.Name is not null)
            {
                System.IO.File.AppendAllText("TODO list.txt", model.Name + " - " + model.Description + Environment.NewLine);
            }
            return RedirectToAction("AddNewTodoToFile");
        }

        public IActionResult TodoItemsListFromFile()
        {
            List<string> t = new List<String>();
            using (StreamReader sr = System.IO.File.OpenText("TODO list.txt"))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    t.Add(s);
                }
            }
            return View(t);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
