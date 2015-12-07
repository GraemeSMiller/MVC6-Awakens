using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.ModelBinding;
using System.Reflection;


namespace MVC6Awakens.Infrastructure.ModelBinders
{
    public class TrimmingSimpleTypeModelBinder : IModelBinder
    {
        public async Task<ModelBindingResult> BindModelAsync(ModelBindingContext bindingContext)
        {
            var result = new ModelBindingResult();
            if (bindingContext.ModelType == typeof(string))
            {
                //Use simple type model binder as base binder
                var simpleTypeModelBinder = new SimpleTypeModelBinder();
                //Get value using simple Type
                result = await simpleTypeModelBinder.BindModelAsync(bindingContext);
                //Get properies of the top level model
                var property = bindingContext.ModelMetadata.ContainerType.GetRuntimeProperties().FirstOrDefault(p => p.Name == bindingContext.ModelName);
                //Get custom attributes
                var attributes = ModelAttributes.GetAttributesForProperty(bindingContext.ModelType, property);
                //Does it have no trim attribute?
                var trimAllowed = attributes.Attributes.OfType<NoTrimAttribute>().FirstOrDefault()==null;
               //If we managed to bind, and got a non null value and we are allowed to trim. Intercept the string value and trim it
                if (result.IsModelSet && result.Model != null && trimAllowed)
                {
                    return await ModelBindingResult.SuccessAsync(bindingContext.ModelName, ((string)result.Model).Trim());
                }
            }
            return result;
        }
    }
}