using BestFor.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

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
        public DbSet<AnswerVote> AnswerVotes { get; set; }
        public DbSet<AnswerDescriptionVote> AnswerDescriptionVotes { get; set; }

        public BestDataContext(DbContextOptions<BestDataContext> options)
            : base(options)
        { }

        public BestDataContext() { }

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
        /// This is needed to set connection string for migrations.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <remarks>
        /// This fires first.
        /// </remarks>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var path = Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            var configuration = builder.Build();
            // Going dumb
            string computerName = System.Environment.GetEnvironmentVariable("COMPUTERNAME");
            string connectionString = configuration["Data:DefaultConnection:ConnectionString"];
            // Overwrite default connection string by computername
            // throw new System.Exception("klsdjlsdfkjsdf " + computerName);
            if (!string.IsNullOrEmpty(computerName))
            {
                string alterConnectionString = configuration["Data:" + computerName + "Connection:ConnectionString"];
                //throw new System.Exception("klsdjlsdfkjsdf " + alterConnectionString);
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

        ///// <summary>
        ///// Now I really lost the track of when this is called.
        ///// For not from tests. I checked. For sure not from web.
        ///// Probably when used from executable that I had at some point and then deleted.
        ///// </summary>
        ///// <param name="services"></param>
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddEntityFramework()
        //        .AddSqlServer()
        //        .AddDbContext<BestFor.Data.BestDataContext>(); // (options =>
        //}

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    foreach (var entity in builder.Model.GetEntityTypes())
        //    {
        //        entity.Relational().TableName = entity.DisplayName();
        //    }

        //    base.OnModelCreating(builder);
        //}
    }
}
