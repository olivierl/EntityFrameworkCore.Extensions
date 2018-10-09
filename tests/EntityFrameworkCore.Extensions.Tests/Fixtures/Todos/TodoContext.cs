using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Extensions.Tests.Fixtures.Todos
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        { }

        public DbSet<Todo> Todos { get; set; }
    }
}