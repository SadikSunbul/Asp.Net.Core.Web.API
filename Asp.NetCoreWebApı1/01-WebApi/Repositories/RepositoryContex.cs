using _01_WebApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace _01_WebApi.Repositories
{
    public class RepositoryContex:DbContext
    {
        public RepositoryContex(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
