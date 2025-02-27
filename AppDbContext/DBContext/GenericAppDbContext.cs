using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AppDbContext.DBContext
{
    public class GenericDbContext<T> : DbContext where T : class
    {
        public GenericDbContext(DbContextOptions<GenericDbContext<T>> options) : base(options) { }

        public DbSet<T> Entities => Set<T>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
