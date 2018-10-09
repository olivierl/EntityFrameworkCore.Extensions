using System;

namespace EntityFrameworkCore.Extensions.Tests.Fixtures.Todos
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; }
    }
}