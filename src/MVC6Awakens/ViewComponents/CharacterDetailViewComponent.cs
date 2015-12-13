using Microsoft.AspNet.Mvc;
using MVC6Awakens.Models;
using System;
using System.Linq;


namespace MVC6Awakens.ViewComponents
{
    [ViewComponent(Name = "CharacterDetailRenamed")]
    public class CharacterDetailViewComponent : ViewComponent
    {
        private readonly DomainContext db;

        public CharacterDetailViewComponent(DomainContext context)
        {
            db = context;
        }

        public IViewComponentResult Invoke(Guid characterId)
        {
            var character = db.Characters.SingleOrDefault(x => x.Id == characterId);
            return View(character);
        }
    }
}
