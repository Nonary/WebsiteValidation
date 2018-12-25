using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FakeDatabase
    {
        public List<User> Users { get; set; } = new List<User>
        {
            new User
            {
                FirstName = "Linda",
                LastName = "Johnson",
                EmailAddress = "micaela1997@hotmail.com",
                Country = "United States",
                State = "Philadelphia",
                DateOfBirth = DateTime.Parse("6/26/1976")
                
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


        public IActionResult Index()
        {
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult CreateUser()
        {
            return PartialView(_database.CurrentUser);
        }


        [HttpPost]
        public IActionResult SubmitUser(User model)
        {
            if (ModelState.IsValid) _database.Users.Add(model);

            return RedirectToAction("Index");
        }

        [AcceptVerbs("POST","GET")]
        public async Task<JsonResult> ValidateEmailAddress(string emailAddress)
        {
            //Simulating server response time
            await Task.Delay(1000);

            if (_database.Users.Any(x => x.EmailAddress == emailAddress))
            {
                return Json("That e-mail address is already in use, please provide a different one.");
            }
            else
            {
                return Json(true);
            }

        }

        [HttpPost]
        public JsonResult GetStates(string country)
        {
            if (country == "Canada")
                return CanadaStates();
            if (country == "United States") return UnitedStates();

            return null;
        }

        private JsonResult UnitedStates()
        {
            return new JsonResult(new[]
            {
                "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut",
                "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa",
                "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan",
                "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "NewHampshire",
                "NewJersey", "NewMexico", "NewYork", "NorthCarolina", "NorthDakota", "Ohio",
                "Oklahoma", "Oregon", "Pennsylvania", "RhodeIsland", "SouthCarolina", "SouthDakota",
                "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "WestVirginia",
                "Wisconsin", "Wyoming"
            });
        }

        private JsonResult CanadaStates()
        {
            return new JsonResult(new[]
            {
                "Quebec", "Nova Scotia", "New Brunswick", "Manitoba", "British Columbia",
                "Prince Edward Island", "Saskatchewan", "Alberta", "Newfoundland and Labrador"
            });
        }
    }
}