using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNet.Mvc;

using MVC6Awakens.Models;


namespace MVC6Awakens.ApiControllers
{
    [Route("api/WeaponOfChoice")]
    public class WeaponOfChoiceController
    {
        private readonly DomainContext context;

        public WeaponOfChoiceController(DomainContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return context.Characters.Select(a => a.WeaponOfChoice).Distinct().OrderBy(a => a);
        }
    }
}
