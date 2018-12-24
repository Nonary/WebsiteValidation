using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FakeDatabase
    {
        public List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                FirstName = "Chase",
                LastName = "Payne",
                EmailAddress = "chase_payne@outlook.com"
            }
        };

        public User CurrentUser { get; set; } = new User();
    }

    public class HomeController : Controller
    {
        private readonly FakeDatabase _database;

        public HomeController(FakeDatabase database)
        {
            _database = database;
        }
        //public IActionResult Index()
        //{
        //    return View(_database);
        //}


        public IActionResult Index(User model)
        {
            _database.CurrentUser = model;
            return View(_database);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> ValidateFields(User model)
        {
            await Task.Delay(1000);
            return RedirectToAction("Index", model);
        }

        public IActionResult CurrentUsers()
        {
            return PartialView(_database.Users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult CreateUser()
        {
            return PartialView(_database.CurrentUser);
        }


        public async Task<JsonResult> GetSampleJson()
        {
            await Task.Delay(1000);
            return new JsonResult(new
            {
                Successful = new Random().Next() % 2 == 0 ? "OK" : "Nope",
                StatusCode = "Didn't work"
            });
        }

        [HttpPost]
        public IActionResult SubmitUser(User model)
        {
            _database.Users.Add(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetStates(Country country)
        {
            if (country.Name == "Canada")
                return CanadaStates();
            return UnitedStates();
        }

        private JsonResult UnitedStates()
        {
            return new JsonResult(new[]
            {
                "Alabama", " Alaska", " Arizona", " Arkansas", " California", " Colorado", " Connecticut",
                " Delaware", " Florida", " Georgia", " Hawaii", " Idaho", " Illinois", " Indiana", " Iowa",
                " Kansas", " Kentucky", " Louisiana", " Maine", " Maryland", " Massachusetts", " Michigan",
                " Minnesota", " Mississippi", " Missouri", " Montana", " Nebraska", " Nevada", " New Hampshire",
                " New Jersey", " New Mexico", " New York", " North Carolina", "  North Dakota", " Ohio",
                " Oklahoma", " Oregon", " Pennsylvania", " Rhode Island", " South Carolina", " South Dakota",
                " Tennessee", " Texas", " Utah", " Vermont", " Virginia", " Washington", " West Virginia",
                " Wisconsin", " Wyoming"
            });
        }

        private JsonResult CanadaStates()
        {
            return new JsonResult(new[]
            {
                "Quebec", " Nova Scotia", " New Brunswick", " Manitoba", " British Columbia",
                " Prince Edward Island", " Saskatchewan", " Alberta", " Newfoundland and Labrador"
            });

        }
    }
}