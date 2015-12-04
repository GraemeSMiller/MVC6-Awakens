using System;

using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.Data.Entity;
using MVC6Awakens.Models;

namespace MVC6Awakens.Controllers
{
    public class PlanetsController : Controller
    {
        private DomainContext context;

        public PlanetsController(DomainContext context)
        {
            this.context = context;    
        }

        // GET: Planets
        public async Task<IActionResult> Index()
        {
            return View(await context.Planets.ToListAsync());
        }

        // GET: Planets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Planet planet = await context.Planets.SingleAsync(m => m.Id == id);
            if (planet == null)
            {
                return HttpNotFound();
            }

            return View(planet);
        }

        // GET: Planets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Planets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Planet planet)
        {
            if (ModelState.IsValid)
            {
                planet.Id = Guid.NewGuid();
                context.Planets.Add(planet);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(planet);
        }

        // GET: Planets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Planet planet = await context.Planets.SingleAsync(m => m.Id == id);
            if (planet == null)
            {
                return HttpNotFound();
            }
            return View(planet);
        }

        // POST: Planets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Planet planet)
        {
            if (ModelState.IsValid)
            {
                context.Update(planet);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(planet);
        }

        // GET: Planets/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Planet planet = await context.Planets.SingleAsync(m => m.Id == id);
            if (planet == null)
            {
                return HttpNotFound();
            }

            return View(planet);
        }

        // POST: Planets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Planet planet = await context.Planets.SingleAsync(m => m.Id == id);
            context.Planets.Remove(planet);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
