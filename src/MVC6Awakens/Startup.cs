using FluentValidation;

using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc.Filters;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using MVC6Awakens.Infrastructure;
using MVC6Awakens.Infrastructure.AutoMapper;
using MVC6Awakens.Infrastructure.FluentValidationBETA;
using MVC6Awakens.Infrastructure.Middleware;
using MVC6Awakens.Infrastructure.ModelBinders;
using MVC6Awakens.Infrastructure.Security;
using MVC6Awakens.Models;
using MVC6Awakens.Services;

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
            services.AddCors();


            //services.AddCors();
            services.AddMvc().AddMvcOptions(
                options =>
                    {
                        options.ModelBinders.Insert(0, new TrimmingSimpleTypeModelBinder());
                        options.Filters.Add(new AuthorizeFilter(defaultPolicy));
                        options.ModelMetadataDetailsProviders.Add(new HumanizerMetadataProvider());
                    }).AddFluentValidation();

            // Add application services.
            services.AddTransient<IValidatorFactory, MvcValidatorFactory>();
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
        }


        public void ConfigureSecurity(IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    "ManageCharacters",
                    authBuilder =>
                    {
                        authBuilder.RequireClaim("ManageCharacters", "Allowed");
                    });
            });
            services.AddInstance<IAuthorizationHandler>(new CharacterAuthorizationHandler());
            //services.Configure<AuthorizationOptions>(options =>
            //{
            //    options.AddPolicy("AllowProfileManagement", policy => policy.Requirements.Add(
            //        services.BuildServiceProvider().GetRequiredService<AllowProfileManagementRequirement>()));
            //});
        }


        //This method is invoked when ASPNET_ENV is 'Development' or is not defined
        //The allowed values are Development,Staging and Production
        //public void ConfigureDevelopment(IApplicationBuilder app, ILoggerFactory loggerFactory)
        //{
        //    loggerFactory.AddConsole(minLevel: LogLevel.Warning);

        //Helps with auto page reloading and intergration with VS
        //app.UseBrowserLink();
        //        //Get useful information for exceptions
        //        app.UseDeveloperExceptionPage();
        //        //Displays database related error details
        //        app.UseDatabaseErrorPage();
        //        // Add the runtime information page that can be used by developers
        //        // to see what packages are used by the application
        //        // default path is: /runtimeinfo
        //        app.UseRuntimeInfoPage();

        //    Configure(app);
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug(LogLevel.Information);
            //app.UseMiddleware<FeelTheForceMiddleware>();
            app.UseFeelTheForce();
            if (env.IsDevelopment())
            {
                //Helps with auto page reloading and intergration with VS
                app.UseBrowserLink();
                //Get useful information for exceptions
                app.UseDeveloperExceptionPage();
                //Displays database related error details
                app.UseDatabaseErrorPage();
                // Add the runtime information page that can be used by developers
                // to see what packages are used by the application
                // default path is: /runtimeinfo
                app.UseRuntimeInfoPage();
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

            //app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            //Alllow hosting static files
            app.UseStaticFiles();

            //Enabled Asp.Net Identity
            app.UseIdentity();

            //// To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseCors(a => a.AllowAnyOrigin());
            //Setup routing
            app.UseMvc(routes =>
            {

                routes.UseTypedRouting();
                routes.MapRoute(
      name: "default",
      template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
