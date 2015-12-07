using System;

using FluentValidation;
using System.Linq;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

using MVC6Awakens.ViewModels.Characters;


namespace MVC6Awakens.Infrastructure.FluentValidationBETA
{
    public static class FluentValidationMVCBuilderExtensions
    {
        public static IMvcBuilder AddFluentValidation(this IMvcBuilder mvcBuilder)
        {
            var serviceCollection = mvcBuilder.Services;
            //services.AddCors();
            mvcBuilder.AddMvcOptions(
                options =>
                    {
                        options.ModelValidatorProviders.Add(serviceCollection.BuildServiceProvider().GetRequiredService<IModelValidatorProvider>());
                    });

            var assembly = typeof(FluentValidationMVCBuilderExtensions).GetTypeInfo().Assembly;
            var exportedTypes = assembly.GetExportedTypes();
            var validatorTypes = exportedTypes.Where(a=>a.GetInterfaces().Any(x =>x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>)));
            foreach (var type in validatorTypes)
            {
                //var genericType = type.GetInterfaces().SingleOrDefault(x => x.IsConstructedGenericType && x.GetGenericTypeDefinition() == typeof(IValidator<>));
                //services.AddTransient(type,genericType);
            }
            serviceCollection.AddTransient<IValidator<CharacterCreate>, CharacterCreateValidator>();

            // Add application services.
            serviceCollection.AddTransient<IModelValidatorProvider, FluentValidationModelValidatorProvider>();
            return mvcBuilder;
        }
    }
}
