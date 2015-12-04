using System;
using System.Linq;
using System.Reflection;

using AutoMapper;


namespace MVC6Awakens.Infrastructure.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => GetConfiguration(Mapper.Configuration));
        }

        private static void GetConfiguration(IConfiguration configuration)
        {
            var assembly = typeof(AutoMapperWebConfiguration).GetTypeInfo().Assembly;
            var exportedTypes = assembly.GetExportedTypes();
            foreach (var type in exportedTypes.Where(a => typeof(Profile).IsAssignableFrom(a)))
            {
                configuration.AddProfile(Activator.CreateInstance(type) as Profile);
            }
        }
    }
}