using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZooApp.Models;
using ZooApp.Services;

namespace ZooApp.Controllers
{
    public class ZooController : Controller
    {
        private readonly ZooSqlService _zooSqlService;

        public ZooController(ZooSqlService zooSqlService)
        {
            _zooSqlService = zooSqlService;
        }

        public IActionResult Index()
        {
            var animals = _zooSqlService.GetAll();
            return View(animals);
        }

        public IActionResult AddAnimal()
        {
            return View();
        }

        public ActionResult DeleteAnimal(int id)
        {
            _zooSqlService.Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult EditAnimal(int id)
        {
            ZooModel model = _zooSqlService.Find(id);
            return View(model);
        }

        public IActionResult SendEditData(ZooModel model)
        {
            if (model.Name is not null)
            {
                _zooSqlService.Edit(model);
            }
            return RedirectToAction("Index");
        }

        public IActionResult SendSubmitData(ZooModel model)
        {
            if (model.Name is not null)
            {
                _zooSqlService.Add(model);
            }
            return RedirectToAction("Index");
        }
    }
}
