using System;

namespace EntityFrameworkCore.Extensions.Tests.Fixtures.Todos
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; }
    }
}