using System.Collections.Generic;

using Microsoft.AspNet.Mvc.Rendering;


namespace MVC6Awakens.Infrastructure.Dropdowns
{
    public interface IDropdownQuery : IQuery<IEnumerable<SelectListItem>> { }
}