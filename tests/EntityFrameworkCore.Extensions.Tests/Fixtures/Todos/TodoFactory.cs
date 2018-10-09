using System;
using System.Collections.Generic;

namespace EntityFrameworkCore.Extensions.Tests.Fixtures.Todos
{
    public static class TodoFactory
    {
        public static IEnumerable<Todo> GetTodos()
        {
            return new List<Todo>
            {
                new Todo { Id = 1, Title = "Todo 1", Done = false, DueDate = new DateTime(2018, 7, 1) },
                new Todo { Id = 2, Title = "My Todo 2", Done = true, DueDate = new DateTime(2018, 6, 1) },
                new Todo { Id = 3, Title = "My Todo 3" },
                new Todo { Id = 4, Title = "Todo 4", Done = false, DueDate = new DateTime(2018, 4, 1) },
                new Todo { Id = 5, Title = "Todo 5", Done = true, DueDate = new DateTime(2018, 10, 1) }
            };
        }
    }
}