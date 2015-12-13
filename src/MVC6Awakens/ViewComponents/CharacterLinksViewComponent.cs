using Microsoft.AspNet.Mvc;
using MVC6Awakens.Models;
using System;
using System.Linq;
using Microsoft.AspNet.Authorization;
using MVC6Awakens.Views.Shared.Components.CharacterDetailRenamed;
using System.Threading.Tasks;
using MVC6Awakens.Infrastructure.Security;

namespace MVC6Awakens.ViewComponents
{
    [ViewComponent(Name = "CharacterLinkGenerator")]
    public class CharacterLinksViewComponent : ViewComponent
    {
        private IAuthorizationService auth;
        private readonly DomainContext db;

        public CharacterLinksViewComponent(DomainContext context, IAuthorizationService auth)
        {
            this.auth = auth;
            db = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid characterId)
        {
            var character = db.Characters.SingleOrDefault(x => x.Id == characterId);
            var vm = new CharacterLinkDetail();
            vm.Edit = await auth.AuthorizeAsync(HttpContext.User, character, CharacterOperations.Manage);
            vm.Id = character.Id;
            return View(vm);
        }
    }
}
