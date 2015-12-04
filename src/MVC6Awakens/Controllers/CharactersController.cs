using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using MVC6Awakens.Models;

namespace MVC6Awakens.Controllers
{
    public class CharactersController : Controller
    {
        private DomainContext context;

        public CharactersController(DomainContext context)
        {
            this.context = context;    
        }

        // GET: Characters
        public IActionResult Index()
        {
            var domainContext = context.Characters.Include(c => c.HomePlanet);
            return View(domainContext.ToList());
        }

        // GET: Characters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var character = context.Characters.Single(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }

            return View(character);
        }

        // GET: Characters/Create
        public IActionResult Create()
        {
            var planets = context.Planets;
            var selectList = new SelectList(planets, "Id", "Name");
            ViewData["PlanetId"] = selectList;
            return View();
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Character character)
        {
            if (ModelState.IsValid)
            {
                character.Id = Guid.NewGuid();
                context.Characters.Add(character);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var planets = context.Planets;
            var selectList = new SelectList(planets, "Id", "Name", character.PlanetId);
            ViewData["PlanetId"] = selectList;
            return View(character);
        }

        // GET: Characters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var character = context.Characters.Single(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }
            var planets = context.Planets;
            var selectList = new SelectList(planets, "Id", "Name", character.PlanetId);
            ViewData["PlanetId"] = selectList;
            return View(character);
        }

        // POST: Characters/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Character character)
        {
            if (ModelState.IsValid)
            {
                context.Update(character);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var planets = context.Planets;
            var selectList = new SelectList(planets, "Id", "Name", character.PlanetId);
            ViewData["PlanetId"] = selectList;
            return View(character);
        }

        // GET: Characters/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var character = context.Characters.Single(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }

            return View(character);
        }

        // POST: Characters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var character = context.Characters.Single(m => m.Id == id);
            context.Characters.Remove(character);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
