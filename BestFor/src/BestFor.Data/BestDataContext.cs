using BestFor.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.Entity;

namespace BestFor.Data
{
    public class BestDataContext : DbContext
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

        public void DebugCallOnConfiguring()
        {
            OnConfiguring(null);
        }
    }
}
