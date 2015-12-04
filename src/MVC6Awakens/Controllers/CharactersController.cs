using System;
using System.Linq;

using AutoMapper;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using MVC6Awakens.Models;
using MVC6Awakens.ViewModels.Characters;
using AutoMapper.QueryableExtensions;

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
            // Automapper projection magic
            var characters = context.Characters.ProjectTo<CharacterDetail>().ToList();
            return View(characters);
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
            ViewData["HomePlanetId"] = selectList;
            return View(new CharacterCreate());
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CharacterCreate characterCreate)
        {
            if (ModelState.IsValid)
            {
                var character = Mapper.Map<Character>(characterCreate);
                context.Characters.Add(character);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var planets = context.Planets;
            var selectList = new SelectList(planets, "Id", "Name", characterCreate.HomePlanetId);
            ViewData["HomePlanetId"] = selectList;
            return View(characterCreate);
        }

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
            var selectList = new SelectList(planets, "Id", "Name", character.HomePlanetId);
            ViewData["HomePlanetId"] = selectList;
            return View(Mapper.Map<CharacterEdit>(character));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CharacterEdit characterEdit)
        {
            if (ModelState.IsValid)
            {
                var character = Mapper.Map<Character>(characterEdit);
                context.Update(character);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            var planets = context.Planets;
            var selectList = new SelectList(planets, "Id", "Name", characterEdit.HomePlanetId);
            ViewData["HomePlanetId"] = selectList;
            return View(characterEdit);
        }

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
