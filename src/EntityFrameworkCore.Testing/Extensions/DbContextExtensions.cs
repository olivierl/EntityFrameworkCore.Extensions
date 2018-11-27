using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Testing.Extensions
{
    public static class DbContextExtensions
    {
        public static DbSet<T> Reload<T>(this DbContext dbContext) where T : class
        {
            var entries = dbContext.ChangeTracker.Entries<T>().ToList();

            foreach (var entityEntry in entries)
                entityEntry.State = EntityState.Detached;
            
            return dbContext.Set<T>();
        }
    }
}