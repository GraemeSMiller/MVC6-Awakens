using System.Collections.Generic;
using System.Linq;


namespace MVC6Awakens.Infrastructure
{
    public static class FeatureFolderExtensions
    {
        public static IEnumerable<string> MoveViewsIntoFeaturesFolder(this IEnumerable<string> viewLocations)
        {
            // I added an additional "App" folder in my MVC project to separate the root configuration 
            // and package files from my application code. 
            return viewLocations.Select(f => f.Replace("/Views/", "/App/Features/"));
        }
    }
}