using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using AutoMapper;

using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using MVC6Awakens.Models;
using MVC6Awakens.ViewModels.Characters;
using AutoMapper.QueryableExtensions;

using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;

using MVC6Awakens.Infrastructure.Security;


namespace MVC6Awakens.Controllers
{
    public class CharactersController : Controller
    {
        private DomainContext context;

        private readonly IAuthorizationService authorizationService;


        public CharactersController(DomainContext context, IAuthorizationService authorizationService)
        {
            this.context = context;
            this.authorizationService = authorizationService;
        }


        // GET: Characters
        public IActionResult Index()
        {
            // Automapper projection magic
            var characters = context.Characters.Where(a => a.Visible).ProjectTo<CharacterDetail>().ToList();
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
            ViewData["HomePlanetId"] = new SelectList(context.Planets, "Id", "Name");
            ViewData["SpeciesId"] = new SelectList(context.Species, "Id", "Name");
            return View(new CharacterCreate());
        }

        // POST: Characters/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CharacterCreate characterCreate)
        {
            var canCreate = await authorizationService.AuthorizeAsync(
                HttpContext.User,
                characterCreate,
                CharacterOperations.Create);
            if (!canCreate)
            {
                return new ChallengeResult();
            }
            if (ModelState.IsValid)
            {
                var character = Mapper.Map<Character>(characterCreate);
                context.Characters.Add(character);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["HomePlanetId"] = new SelectList(context.Planets, "Id", "Name", characterCreate.HomePlanetId);
            ViewData["SpeciesId"] = new SelectList(context.Species, "Id", "Name", characterCreate.SpeciesId);
            return View(characterCreate);
        }

        public async Task<IActionResult> Edit(Guid? id)
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
            var canEdit = await authorizationService.AuthorizeAsync(
    HttpContext.User,
    character,
    CharacterOperations.Create);
            if (!canEdit)
            {
                return new ChallengeResult();
            }

            ViewData["HomePlanetId"] = new SelectList(context.Planets, "Id", "Name", character.HomePlanetId);
            ViewData["SpeciesId"] = new SelectList(context.Species, "Id", "Name", character.SpeciesId);
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
            ViewData["HomePlanetId"] = new SelectList(context.Planets, "Id", "Name", characterEdit.HomePlanetId);
            ViewData["SpeciesId"] = new SelectList(context.Species, "Id", "Name", characterEdit.SpeciesId);
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
