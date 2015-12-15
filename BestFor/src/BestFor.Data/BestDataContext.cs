using System.Threading;
using System.Threading.Tasks;
using BestFor.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;

namespace BestFor.Data
{
    public class BestDataContext : DbContext, IDataContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

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

        public Task<int> SaveChangesAsync()
        {
            return new Task<int>(() => default(int));
        }
    }
}
