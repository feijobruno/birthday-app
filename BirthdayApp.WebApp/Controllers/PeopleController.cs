using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BirthdayApp.Data;
using BirthdayApp.Business;

namespace BirthdayApp.WebApp.Controllers
{
    public class PeopleController : Controller
    {
        public PeopleController()
        {
            Db = new PeopleRepositoryMemory();
        }

        private readonly PeopleRepository Db;

        // GET: People
        public ActionResult Index()
        {
            var people = Db.GetAllPeople();
            return View(people);
        }

        // GET: People/Details/5
        public ActionResult Details(int id)
        {
            var person = Db.GetPersonById(id);
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: People/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Person person)
        {
            try
            {
                // TODO: Add insert logic here
                Db.SalvePerson(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: People/Edit/5
        public ActionResult Edit(int id)
        {
            var person = Db.GetPersonById(id);
            return View(person);
        }

        // POST: People/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Person person)
        {
            try
            {
                // TODO: Add update logic here

                Db.SalvePerson(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: People/Delete/5
        public ActionResult Delete(int id)
        {
            var person = Db.GetPersonById(id);
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Person person)
        {
            try
            {
                // TODO: Add delete logic here
                Db.DeletePerson(person);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}