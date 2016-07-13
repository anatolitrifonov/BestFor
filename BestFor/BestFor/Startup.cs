using BestFor.Data;
using BestFor.Domain.Entities;
using BestFor.Services.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using React.AspNet;
using NLog.Extensions.Logging;

namespace BestFor
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddDbContext<BestDataContext>();
            //services.AddDbContext<BestDataContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

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
            services.AddAntiforgery(options => options.CookieName = Controllers.BaseApiController.ANTI_FORGERY_COOKIE_NAME);

            // Register application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();

            services.AddMemoryCache();

            // Add Application settings to the services container.
            // services.Configure<BestFor.Common.AppSettings>(Configuration.GetSection("AppSettings"));

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // loggerFactory.MinimumLevel = LogLevel.Information;

            loggerFactory.AddNLog();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                // app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //// Initialise ReactJS.NET. Must be before static files.
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


            app.UseStaticFiles();

            // Add cookie-based authentication to the request pipeline.
            app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

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
    }
}
