using System;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Testing
{
    public class TestDatabase<TContext> : IDisposable where TContext : DbContext
    {
        public readonly SqliteConnection Connection;

        public TestDatabase()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
        }

        public async Task<DbContextOptions<TContext>> InitializeAsync(Func<TContext, Task> seeder = null)
        {
            var options = new DbContextOptionsBuilder<TContext>()
                .UseSqlite(Connection)
                .Options;
            
            // Create the schema in the database
            await using (var context = (TContext)Activator.CreateInstance(typeof(TContext), options))
            {
                context.Database.EnsureCreated();
            }

            // Insert seed data into the database using one instance of the context
            await using (var context = (TContext)Activator.CreateInstance(typeof(TContext), options))
            {
                await seeder?.Invoke(context);
            }

            return options;
        }

        public void Dispose()
        {
            Connection.Close();
        }
    }
}