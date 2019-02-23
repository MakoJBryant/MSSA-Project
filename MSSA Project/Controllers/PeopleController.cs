using MSSA_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MSSA_Project.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        public ActionResult Index()
        {
            PersonModel p = new PersonModel();
            int i = 5 / p.Age;

            return View();
        }

        public ActionResult ListPeople()
        {
            List <PersonModel> people = new List<PersonModel>();

            people.Add(new PersonModel { FirstName = "Mako", LastName = "Bryant", Age = 42 });
            people.Add(new PersonModel { FirstName = "Liam", LastName = "Bryant", Age = 69 });
            people.Add(new PersonModel { FirstName = "Emma", LastName = "Johnson", Age = 73 });

            return View(people);
        }
    }
}