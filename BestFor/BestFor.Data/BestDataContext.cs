using BestFor.Domain.Entities;
using BestFor.Domain.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace BestFor.Data
{
    [ExcludeFromCodeCoverage]
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

            // This is right now the only way to stick in the unique index that is not a key into the database.
            builder.Entity<Suggestion>().HasIndex(b => b.Phrase).IsUnique();
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
                if (!string.IsNullOrEmpty(alterConnectionString))
                    connectionString = alterConnectionString;
            }
            // See if we can override from anothe env variable.
            string conectionStringFromEnv = System.Environment.GetEnvironmentVariable("main_database_connection");
            if (!string.IsNullOrEmpty(conectionStringFromEnv))
                connectionString = conectionStringFromEnv;

            optionsBuilder.UseSqlServer(connectionString);
        }

        /// <summary>
        /// Give direct access to sets. Should be limited to may be internal but then it is not clear how to test it.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public virtual DbSet<TEntity> EntitySet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// This was taken from some implementation of unit of work.
        /// But then EF in .Net is a bit bette and has its own state tracking.
        /// I left the methos just in case but most probably it is not needed.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState
        {
            // var t = Entry(entity).State;
            // var state = StateHelper.ConvertState(entity.ObjectState);
            // Do nothing. 
            // Entry(entity).State = state;
        }

    }
}
