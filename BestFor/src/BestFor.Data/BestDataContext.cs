using BestFor.Domain.Entities;

using Microsoft.Data.Entity;

namespace BestFor.Data
{
    public class BestDataContext : DbContext
    {
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=Blogging;integrated security=True;");
        }

        //protected override void OnConfiguring(DbContextOptions options)
        //{
        //    options.UseSqlServer(Startup.Configuration.Get("Data:DefaultConnection:ConnectionString"));

        //}
    }
}
