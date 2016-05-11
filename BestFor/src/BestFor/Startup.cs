using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Services.Messaging;
using Microsoft.AspNet.Authentication.Facebook;
using Microsoft.AspNet.Authentication.Google;
using Microsoft.AspNet.Authentication.MicrosoftAccount;
using Microsoft.AspNet.Authentication.Twitter;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Diagnostics.Entity;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Localization;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using NLog.Extensions.Logging;
using React.AspNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BestFor
{
    public class Startup
    {
        /// <summary>
        /// Save the location since it does not seem to be a way to get there in Configure.
        /// </summary>
        private string _applicationBasePath { get; set; }

        /// <summary>
        /// Entry point to any ASP.NET 5 application and configures basic feature support.
        /// </summary>
        /// <param name="env"></param>
        /// <param name="appEnv"></param>
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {
            _applicationBasePath = appEnv.ApplicationBasePath;

            // Include application settings file.
            var builder = new ConfigurationBuilder()
                .SetBasePath(_applicationBasePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // This reads the configuration keys from the secret store.
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by a runtime.
        // Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Entity Framework services to the services container.
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BestDataContext>(); // (options =>
                                                  // options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            // Add Identity services to the services container.
            // This enabled injection of UserManager<ApplicationUser> and SignInManager<ApplicationUser> for AccountController
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Cookies.ApplicationCookie.AccessDeniedPath = new PathString("/Account/AccessDenied");
                })
                .AddEntityFrameworkStores<BestDataContext>()
                .AddDefaultTokenProviders();

            // Add React service
            services.AddReact();

            // Add MVC services to the services container.
            services.AddMvc().AddViewLocalization().AddDataAnnotationsLocalization();

            // Register action filter that deals with localization.
            services.AddScoped<LanguageActionFilter>();

            // Uncomment the following line to add Web API services which makes it easier to port Web API 2 controllers.
            // You will also need to add the Microsoft.AspNet.Mvc.WebApiCompatShim package to the 'dependencies' section of project.json.
            // services.AddWebApiConventions();

            // We added MVC -> we get Antiforgery enabled
            // Now just tweak configuration a bit so that we get more controler over cookie.
            services.ConfigureAntiforgery(options => options.CookieName = Controllers.BaseApiController.ANTI_FORGERY_COOKIE_NAME);

            // Register application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddCaching();

            // Add Application settings to the services container.
            services.Configure<BestFor.Common.AppSettings>(Configuration.GetSection("AppSettings"));

            // Add my services
            // I do not want to add "using" for all projects in solution just to keep the list of fusings clean.
            services.AddScoped<BestFor.Data.IDataContext, BestFor.Data.BestDataContext>();
            services.AddScoped<BestFor.Data.IAnswerRepository, BestFor.Data.AnswerRepository>();
            services.AddScoped<BestFor.Data.IAnswerDescriptionRepository, BestFor.Data.AnswerDescriptionRepository>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.Suggestion>, BestFor.Data.Repository<BestFor.Domain.Entities.Suggestion>>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.BadWord>, BestFor.Data.Repository<BestFor.Domain.Entities.BadWord>>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.ResourceString>, BestFor.Data.Repository<BestFor.Domain.Entities.ResourceString>>();
            // services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.AnswerDescription>, BestFor.Data.Repository<BestFor.Domain.Entities.AnswerDescription>>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.AnswerFlag>, BestFor.Data.Repository<BestFor.Domain.Entities.AnswerFlag>>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.AnswerDescriptionFlag>, BestFor.Data.Repository<BestFor.Domain.Entities.AnswerDescriptionFlag>>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.AnswerVote>, BestFor.Data.Repository<BestFor.Domain.Entities.AnswerVote>>();
            services.AddScoped<BestFor.Data.IRepository<BestFor.Domain.Entities.AnswerDescriptionVote>, BestFor.Data.Repository<BestFor.Domain.Entities.AnswerDescriptionVote>>();
            services.AddScoped<BestFor.Services.Cache.ICacheManager, BestFor.Services.Cache.CacheManager>();
            services.AddScoped<BestFor.Services.Services.IProfanityService, BestFor.Services.Services.ProfanityService>();
            services.AddScoped<BestFor.Services.Services.IAnswerService, BestFor.Services.Services.AnswerService>();
            services.AddScoped<BestFor.Services.Services.ISuggestionService, BestFor.Services.Services.SuggestionService>();
            services.AddScoped<BestFor.Services.Services.IStatusService, BestFor.Services.Services.StatusService>();
            services.AddScoped<BestFor.Services.Services.IResourcesService, BestFor.Services.Services.ResourcesService>();
            services.AddScoped<BestFor.Services.Services.IAnswerDescriptionService, BestFor.Services.Services.AnswerDescriptionService>();
            // TODO: For now we use specific implementation. Might need a different injection later when we have more than one service.
            services.AddScoped<BestFor.Services.Services.IProductService, BestFor.Services.AffiliateProgram.Amazon.AmazonProductService>();
            services.AddScoped<BestFor.Services.Services.IFlagService, BestFor.Services.Services.FlagService>();
            services.AddScoped<BestFor.Services.Services.IVoteService, BestFor.Services.Services.VoteService>();
            services.AddScoped<BestFor.Services.Services.IUserService, BestFor.Services.Services.UserService>();
        }

        // Configure is called after ConfigureServices is called.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.MinimumLevel = LogLevel.Information;
            //loggerFactory.AddConsole();
            //loggerFactory.AddDebug();
            //add NLog to aspnet5
            loggerFactory.AddNLog();
            // configure nlog.config in your project root
            // Does not work that easy ... have to do some jumping around
            env.ConfigureNLog(_applicationBasePath + "\\nlog.config");

            //string z = "";
            //foreach (var t in Configuration.GetChildren())
            //{
            //    z += t.Key + " p " + t.Path + " w " + t.Value + "<br>";
            //}
            
            //app.Run(async (context) =>
            //{
            //    // await context.Response.WriteAsync("Hello World! " + env.IsDevelopment() + "  " + env.WebRootFileProvider.GetFileInfo("nlog.config").PhysicalPath);
            //    await context.Response.WriteAsync("Hello World! " + env.IsDevelopment() + "  " + env.WebRootPath);
            //});

            // Configure the HTTP request pipeline.

            // Add the following to the request pipeline only in development environment.
            //if (env.IsDevelopment())
            // {
            // app.UseBrowserLink();
            // Captures synchronous and asynchronous exceptions from the pipeline and generates HTML error responses. 
            // Full error details are only displayed by default if 'host.AppMode' is set to 'development' 
            // in the IApplicationBuilder.Properties.  
            app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage(options => { options.EnableAll(); });
       //     }
      //      else
       //     {
                // Add Error handling middleware which catches all application specific errors and
                // sends the request to the following path or controller action.
        //        app.UseExceptionHandler("/Home/Error");
         //   }

            // Add the platform handler to the request pipeline.
            app.UseIISPlatformHandler();

            // Initialise ReactJS.NET. Must be before static files.
            app.UseReact(config =>
            {
                // If you want to use server-side rendering of React components,
                // add all the necessary JavaScript files here. This includes
                // your components as well as all of their dependencies.
                // See http://reactjs.net/ for more information. Example:
                //config
                //    .AddScript("~/Scripts/First.jsx")
                //    .AddScript("~/Scripts/Second.jsx");
                config
                    .SetReuseJavaScriptEngines(false)
                    .SetAllowMsieEngine(false)
                    .SetLoadBabel(true)
                    .AddScript("~/Scripts/MenuControl.jsx")
                    .AddScript("~/Scripts/SuggestionControl.jsx")
                    .AddScript("~/Scripts/SuggestionLineItem.jsx")
                    .AddScript("~/Scripts/SuggestionAnswerItem.jsx")
                    .AddScript("~/Scripts/SuggestionResultList.jsx")
                    .AddScript("~/Scripts/SuggestionAnswerList.jsx")
                    .AddScript("~/Scripts/SuggestionPanel.jsx")
                    .AddScript("~/Scripts/SuggestionTextBox.jsx")
                    .AddScript("~/Scripts/AffiliateProductDetails.jsx");

                // If you use an external build too (for example, Babel, Webpack,
                // Browserify or Gulp), you can improve performance by disabling
                // ReactJS.NET's version of Babel and loading the pre-transpiled
                // scripts. Example:
                //config
                //    .SetLoadBabel(false)
                //    .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
            });

            // Configure the HTTP request pipeline.
            // AT: <- This allows serving static files
            app.UseStaticFiles();

            // Turn on directory browsing for the current directory.
            // app.UseDirectoryBrowser();

            // Serve the default file, if present.
            // AT: <- Enables the capability of serving default page.
            // default.htm
            // default.html
            // index.htm
            // index.html
            // app.UseDefaultFiles();

            // Enable the IIS native module to run after the ASP.NET middleware components.
            // This call should be placed at the end of your Startup.Configure method so that
            // it doesn't interfere with other middleware functionality.
            // app.RunIISPipeline();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

          //   app.UseCookieAuthentication(options => options.CookieName = "gggggggggggggggggggggggggg");

            // Add and configure the options for authentication middleware to the request pipeline.
            // You can add options for middleware as shown below.
            // For more information see http://go.microsoft.com/fwlink/?LinkID=532715
            //app.UseFacebookAuthentication(options =>
            //{
            //    options.AppId = Configuration["Authentication:Facebook:AppId"];
            //    options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //});
            //app.UseGoogleAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:Google:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            //});
            //app.UseMicrosoftAccountAuthentication(options =>
            //{
            //    options.ClientId = Configuration["Authentication:MicrosoftAccount:ClientId"];
            //    options.ClientSecret = Configuration["Authentication:MicrosoftAccount:ClientSecret"];
            //});
            //app.UseTwitterAuthentication(options =>
            //{
            //    options.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //    options.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //});

            // Add MVC to the request pipeline.
            app.UseMvc(routes =>
            {
                // This route checks all the requests to see if this is direct link to a content
                // Constraint will check if {content} is ok to be a "content"
                // Basically it will check if content starts with best and is in format "best<something>for<something>is<something>"
                // If that is the case contraint will say OK and show Home/MyContentAction
                routes.MapRoute(
                        name: "SearchEngineContent",
                        template: "{content}",
                        defaults: new { controller = "Home", action = "MyContent" },
                        constraints: new { constraint = new ContentRouteConstraint() });
                // This route checks all the requests to see if this is direct link to a content
                // Constraint will check if ResourceService "knows" about language + contry combination.
                // If does not know then assume English US
                // It will the check if {content} is ok to be a "content"
                // Basically it will check if content starts with best In specified language
                // and is in format "<best in language><something><for in language><something><is in language><something>"
                // If that is the case contraint will say OK and show Home/MyContentAction
                routes.MapRoute(
                        name: "SearchEngineContentWithCulture",
                        template: "{language}-{country}/{content}",
                        defaults: new { controller = "Home", action = "MyContent" },
                        constraints: new { constraint = new ContentRouteConstraint() });

                // See LocalizationRouteConstraint for description of how this mapping works.
                routes.MapRoute(
                        name: "DefaultWithCulture",
                        template: "{language}-{country}/{controller}/{action}/{id?}",
                        defaults: new { controller = "Home", action = "Index" },
                        constraints: new { mylink = new LocalizationRouteConstraint() });

                routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");


                // Uncomment the following line to add a route for porting Web API 2 controllers.
                // routes.MapWebApiRoute("DefaultApi", "api/{controller}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
