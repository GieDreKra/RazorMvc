using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        private SqlConnection _connection;

        public ZooController(ZooSqlService zooSqlService, SqlConnection connection)
        {
            _zooSqlService = zooSqlService;
            _connection = connection;
        }

        public IActionResult Index()
        {
            var animals = _zooSqlService.GetAll();
            return View(animals);
        }

        public IActionResult ListSponsors()
        {
            var sponsors = _zooSqlService.GetAllSponsors();
            return View(sponsors);
        }

        public IActionResult AddAnimal()
        {
            return View();
        }

        public IEnumerable<ZooModel> GetAnimalList()
        {
            string query = "SELECT [Id],[Name] FROM Zoo.dbo.Zoo";
            IEnumerable<ZooModel> result = _connection.Query<ZooModel>(query);
            return result;
        }

        public IActionResult AddSponsor()
        {
            SponsorModel ZM = new SponsorModel();
            ZM.AnimalList = new SelectList(GetAnimalList(), "Id", "Name"); // model binding
            return View(ZM);
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

        public IActionResult SendSubmitDataSponsor(SponsorModel model)
        {
            if ((model.FirstName is not null) && (model.LastName is not null))
            {
                _zooSqlService.AddSponsor(model);
            }
            return RedirectToAction("Index");
        }
    }
}
