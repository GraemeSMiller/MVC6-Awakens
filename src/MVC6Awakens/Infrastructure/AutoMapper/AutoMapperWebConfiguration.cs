using AutoMapper;


namespace MVC6Awakens.Infrastructure.AutoMapper
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg =>
                                  {
                                      cfg.AddProfile(new UserProfile());
                                  });
        }
    }
}