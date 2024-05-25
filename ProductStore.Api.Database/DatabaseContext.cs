using Microsoft.EntityFrameworkCore;
using ProductStore.Api.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace ProductStore.Api.Repository
{
    /// <summary>
    /// Definition for the database context
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class DatabaseContext : DbContext
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public DatabaseContext()
        {
        }

        /// <summary>
        /// Constructor to include Database configuration
        /// </summary>
        /// <param name="options"></param>
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
