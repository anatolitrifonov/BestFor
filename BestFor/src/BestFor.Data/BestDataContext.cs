using System;
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
        public DbSet<AnswerFlag> AnswerFlags { get; set; }
        public DbSet<AnswerDescriptionFlag> AnswerDescriptionFlags { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <remarks>
        /// This fires second (after OnConfiguring)
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <remarks>
        /// This fires first.
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            // Going dumb
            string computerName = Environment.GetEnvironmentVariable("COMPUTERNAME");
            string connectionString = configuration["Data:DefaultConnection:ConnectionString"];
            // Overwrite default connection string by computername
            if (!string.IsNullOrEmpty(computerName))
            {
                string alterConnectionString = configuration["Data:" + computerName + "Connection:ConnectionString"];
                if (!string.IsNullOrEmpty(alterConnectionString))
                    connectionString = alterConnectionString;
            }
            optionsBuilder.UseSqlServer(connectionString);
        }

        public virtual DbSet<TEntity> EntitySet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            Entry(entity).State = StateHelper.ConvertState(entity.ObjectState);
        }

        /// <summary>
        /// Now I really lost the track of when this is called.
        /// For not from tests. I checked. For sure not from web.
        /// Probably when used from executable that I had at some point and then deleted.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<BestFor.Data.BestDataContext>(); // (options =>
        }
    }
}
