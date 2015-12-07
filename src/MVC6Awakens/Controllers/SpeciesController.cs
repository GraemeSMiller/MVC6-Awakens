using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using MVC6Awakens.Models;

namespace MVC6Awakens.Controllers
{
    public class SpeciesController : Controller
    {
        private readonly DomainContext context;

        public SpeciesController(DomainContext context)
        {
            this.context = context;    
        }

        // GET: Species
        public async Task<IActionResult> Index()
        {
            return View(await context.Species.ToListAsync());
        }

        // GET: Species/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Species species = await context.Species.SingleAsync(m => m.Id == id);
            if (species == null)
            {
                return HttpNotFound();
            }

            return View(species);
        }

        // GET: Species/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Species/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Species species)
        {
            if (ModelState.IsValid)
            {
                species.Id = Guid.NewGuid();
                context.Species.Add(species);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(species);
        }

        // GET: Species/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Species species = await context.Species.SingleAsync(m => m.Id == id);
            if (species == null)
            {
                return HttpNotFound();
            }
            return View(species);
        }

        // POST: Species/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Species species)
        {
            if (ModelState.IsValid)
            {
                context.Update(species);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(species);
        }

        // GET: Species/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Species species = await context.Species.SingleAsync(m => m.Id == id);
            if (species == null)
            {
                return HttpNotFound();
            }

            return View(species);
        }

        // POST: Species/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Species species = await context.Species.SingleAsync(m => m.Id == id);
            context.Species.Remove(species);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
