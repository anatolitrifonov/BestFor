using BestFor.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BestFor.Data
{
    public class BestDataContext : IdentityDbContext<ApplicationUser>, IDataContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<BadWord> BadWords { get; set; }
        public DbSet<AnswerDescription> AnswerDescriptions { get; set; }
        public DbSet<ResourceString> ResourceStrings { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();

            optionsBuilder.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"]);
        }

        public virtual DbSet<TEntity> EntitySet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BestFor.Data.BestDataContext>(); // (options =>
        }
    }
}
