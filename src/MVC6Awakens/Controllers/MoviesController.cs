using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

using Microsoft.Data.Entity;
using MVC6Awakens.Models;

namespace MVC6Awakens.Controllers
{
    public class MoviesController : Controller
    {
        private readonly DomainContext context;

        public MoviesController(DomainContext context)
        {
            this.context = context;    
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(await context.Movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = await context.Movies.SingleAsync(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.Id = Guid.NewGuid();
                context.Movies.Add(movie);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = await context.Movies.SingleAsync(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                context.Update(movie);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Movie movie = await context.Movies.SingleAsync(m => m.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            Movie movie = await context.Movies.SingleAsync(m => m.Id == id);
            context.Movies.Remove(movie);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
