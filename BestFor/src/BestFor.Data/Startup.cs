using Microsoft.Dnx.Runtime;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Hosting;

namespace BestFor.Data
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public void Configure(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer();
             //   .AddDbContext<BestDataContext>(options =>
              //      options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            //services.AddIdentity<ApplicationUser, IdentityRole>()
            //    .AddEntityFrameworkStores<AppContext>();

            //services.AddMvc();
        }
    }
}
