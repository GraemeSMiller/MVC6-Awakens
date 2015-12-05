﻿using FluentValidation;
using FluentValidation.Attributes;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MVC6Awakens.Infrastructure.AutoMapper;
using MVC6Awakens.Infrastructure.FluentValidationBETA;
using MVC6Awakens.Infrastructure.Security;
using MVC6Awakens.Models;
using MVC6Awakens.Services;
using MVC6Awakens.ViewModels.Characters;


namespace MVC6Awakens
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var formattableString = $"appsettings.{env.EnvironmentName}.json";
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile(formattableString, optional: true);

            //Check the environment to see if we are developing
            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            AutoMapperWebConfiguration.Configure();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureSecurity(services);
            // Add framework services.                                                     
            var connectionString = Configuration["Data:DefaultConnection:ConnectionString"];
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<DomainContext>(options =>
                                                 {
                                                     options.UseSqlServer(connectionString);
                                                 })
                .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // only allow authenticated users
            //http://leastprivilege.com/2015/10/12/the-state-of-security-in-asp-net-5-and-mvc-6-authorization/
            var defaultPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

            services.AddMvc().AddMvcOptions(
                options =>
                    {
                        options.Filters.Add(new AuthorizeFilter(defaultPolicy));
                        options.ModelValidatorProviders.Add(services.BuildServiceProvider().GetRequiredService<IModelValidatorProvider>());
                        options.ModelMetadataDetailsProviders.Add(new HumanizerMetadataProvider());
                    });


            services.AddTransient<IValidator<CharacterCreate>, CharacterCreateValidator>();

            // Add application services.
            services.AddTransient<IModelValidatorProvider, FluentValidationModelValidatorProvider>();
            services.AddTransient<IValidatorFactory, MvcValidatorFactory>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }


        public void ConfigureSecurity(IServiceCollection services)
        {
            services.AddInstance<IAuthorizationHandler>(new CharacterAuthorizationHandler());
            services.AddInstance<IAuthorizationHandler>(new CharacterCreateAuthorizationHandler());
            //services.Configure<AuthorizationOptions>(options =>
            //{
            //    options.AddPolicy("AllowProfileManagement", policy => policy.Requirements.Add(
            //        services.BuildServiceProvider().GetRequiredService<AllowProfileManagementRequirement>()));
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Information);

            if (env.IsDevelopment())
            {
                //Helps with auto page reloading and intergration with VS
                app.UseBrowserLink();
                //Get useful information for exceptions
                app.UseDeveloperExceptionPage();
                //Displays database related error details
                app.UseDatabaseErrorPage();
            }
            else
            {
                //Where to go when we get an Exception
                app.UseExceptionHandler("/Home/Error");

                // For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());
            //Alllow hosting static files
            app.UseStaticFiles();

            app.UseCookieAuthentication(options => { options.AutomaticAuthenticate = true; });
            //Enabled Asp.Net Identity
            app.UseIdentity();

            // To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

            //Setup routing
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
