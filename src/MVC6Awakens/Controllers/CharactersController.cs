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
        private readonly DomainContext context;

        private readonly IAuthorizationService authorizationService;


        public CharactersController(DomainContext context, IAuthorizationService authorizationService)
        {
            this.context = context;
            this.authorizationService = authorizationService;
        }


        // GET: Characters
        public async Task<IActionResult> Index()
        {
            var canManage = await authorizationService.AuthorizeAsync(User, "ManageCharacters");
            // Automapper projection magic
            var characters = canManage ? await context.Characters.ProjectTo<CharacterDetail>().ToArrayAsync() : await context.Characters.Where(a => a.Visible || a.CreatorId == User.GetUserId()).ProjectTo<CharacterDetail>().ToArrayAsync();
            return View(characters);
        }

        // GET: Characters/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var character = await context.Characters.Include(a => a.HomePlanet).Include(a => a.Species).SingleAsync(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }

            var characterDetail = Mapper.Map<CharacterDetail>(character);
            return View(characterDetail);
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
            if (ModelState.IsValid)
            {
                var character = Mapper.Map<Character>(characterCreate);
                character.CreatorId = User.GetUserId();
                context.Characters.Add(character);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
                //DEATH TO MAGIC STRINGS
                //https://github.com/ivaylokenov/AspNet.Mvc.TypedRouting
                //return this.RedirectToAction(c => c.Index());
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

            var character = await context.Characters.SingleAsync(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }
            var canEdit = await authorizationService.AuthorizeAsync(HttpContext.User, character, CharacterOperations.Manage);
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
        public async Task<IActionResult> Edit(CharacterEdit characterEdit)
        {
            var character = await context.Characters.SingleAsync(m => m.Id == characterEdit.Id);
            var canEdit = await authorizationService.AuthorizeAsync(HttpContext.User, character, CharacterOperations.Manage);
            if (!canEdit)
            {
                return new ChallengeResult();
            }

            if (ModelState.IsValid)
            {
                Mapper.Map(characterEdit, character);
                context.Update(character);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["HomePlanetId"] = new SelectList(context.Planets, "Id", "Name", characterEdit.HomePlanetId);
            ViewData["SpeciesId"] = new SelectList(context.Species, "Id", "Name", characterEdit.SpeciesId);
            return View(characterEdit);
        }

        [Authorize("ManageCharacters")]
        public async Task<IActionResult> Publish(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var character = await context.Characters.Include(a => a.HomePlanet).Include(a => a.Species).SingleAsync(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<CharacterPublish>(character));
        }
        [Authorize("ManageCharacters")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Publish(CharacterPublish characterPublish)
        {
            if (ModelState.IsValid)
            {
                var character = await context.Characters.SingleAsync(m => m.Id == characterPublish.Id);
                Mapper.Map(characterPublish, character);
                context.Update(character);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(characterPublish);
        }


        [Authorize("ManageCharacters")]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var character = await context.Characters.SingleAsync(m => m.Id == id);
            if (character == null)
            {
                return HttpNotFound();
            }

            return View(character);
        }
        [Authorize("ManageCharacters")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var character = await context.Characters.Include(a => a.HomePlanet).Include(a => a.Species).SingleAsync(m => m.Id == id);
            context.Characters.Remove(character);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
