using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Extensions.Tests.Fixtures;
using EntityFrameworkCore.Extensions.Tests.Fixtures.Todos;
using EntityFrameworkCore.Testing;
using Xunit;

namespace EntityFrameworkCore.Extensions.Tests
{
    // ReSharper disable once InconsistentNaming
    public class IQueryableExtenstionsTests
    {
        private static Dictionary<string, Expression<Func<Todo, object>>> ColumnsMap => new()
        {
            ["id"] = v => v.Id,
            ["title"] = v => v.Title,
            ["dueDate"] = v => v.DueDate!
        };

        [Fact]
        public async Task ApplyOrdering_OrdersByTitlesAsc()
        {
            using var testDb = new TestDatabase<TodoContext>();
            var todos = TodoFactory.GetTodos();

            var options = await testDb.InitializeAsync(async context =>
            {
                context.Todos.AddRange(todos);
                await context.SaveChangesAsync();
            });

            var queryObject = new QueryObject
            {
                SortBy = "title",
                IsSortAscending = true
            };

            using (var context = new TodoContext(options))
            {
                var result = await context.Todos.AsQueryable()
                    .ApplyOrdering(queryObject, ColumnsMap)
                    .ToListAsync();

                Assert.Equal(5, result.Count);
                Assert.Equal("My Todo 2", result.ElementAt(0).Title);
                Assert.Equal("My Todo 3", result.ElementAt(1).Title);
                Assert.Equal("Todo 1", result.ElementAt(2).Title);
                Assert.Equal("Todo 4", result.ElementAt(3).Title);
                Assert.Equal("Todo 5", result.ElementAt(4).Title);
            }
        }

        [Fact]
        public async Task ApplyOrdering_OrdersByTitlesDesc()
        {
            using var testDb = new TestDatabase<TodoContext>();
            var todos = TodoFactory.GetTodos();

            var options = await testDb.InitializeAsync(async context =>
            {
                context.Todos.AddRange(todos);
                await context.SaveChangesAsync();
            });

            await using (var context = new TodoContext(options))
            {
                var result = await context.Todos.AsQueryable()
                    .ApplyOrdering("title", true, ColumnsMap)
                    .ToListAsync();

                Assert.Equal(5, result.Count);
                Assert.Equal("Todo 5", result.ElementAt(0).Title);
                Assert.Equal("Todo 4", result.ElementAt(1).Title);
                Assert.Equal("Todo 1", result.ElementAt(2).Title);
                Assert.Equal("My Todo 3", result.ElementAt(3).Title);
                Assert.Equal("My Todo 2", result.ElementAt(4).Title);
            }
        }

        [Fact]
        public async Task ApplyOrdering_ReturnTodosWhenUnknowColumn()
        {
            using var testDb = new TestDatabase<TodoContext>();
            var todos = TodoFactory.GetTodos();

            var options = await testDb.InitializeAsync(async context =>
            {
                context.Todos.AddRange(todos);
                await context.SaveChangesAsync();
            });

            var queryObject = new QueryObject
            {
                SortBy = "unknown",
                IsSortAscending = true
            };

            await using (var context = new TodoContext(options))
            {
                var result = await context.Todos.AsQueryable()
                    .ApplyOrdering(queryObject, ColumnsMap)
                    .ToListAsync();

                Assert.Equal(5, result.Count);
            }
        }

        [Fact]
        public async Task FindAsync_Paginate_Returns4Todos()
        {
            using var testDb = new TestDatabase<TodoContext>();
            var todos = TodoFactory.GetTodos();

            var options = await testDb.InitializeAsync(async context =>
            {
                context.Todos.AddRange(todos);
                await context.SaveChangesAsync();
            });

            var queryObject = new QueryObject
            {
                SortBy = "id",
                IsSortAscending = true,
                PageSize = 3
            };

            using (var context = new TodoContext(options))
            {
                var result = await context.Todos.AsQueryable()
                    .ApplyOrdering(queryObject, ColumnsMap)
                    .ApplyPaging(queryObject)
                    .ToListAsync();

                Assert.Equal(3, result.Count);
                Assert.Equal(1, result.First().Id);
                Assert.Equal(3, result.Last().Id);
            }
        }

        [Fact]
        public async Task FindAsync_Paginate_ReturnsPage2()
        {
            using var testDb = new TestDatabase<TodoContext>();
            var todos = TodoFactory.GetTodos();

            var options = await testDb.InitializeAsync(async context =>
            {
                context.Todos.AddRange(todos);
                await context.SaveChangesAsync();
            });

            await using (var context = new TodoContext(options))
            {
                var result = await context.Todos.AsQueryable()
                    .ApplyOrdering("id", false, ColumnsMap)
                    .ApplyPaging(2, 3)
                    .ToListAsync();

                Assert.Equal(2, result.Count);
                Assert.Equal(4, result.First().Id);
                Assert.Equal(5, result.Last().Id);
            }
        }

        [Fact]
        public async Task FindAsync_PaginateWithWrongData_ReturnsDefault()
        {
            using var testDb = new TestDatabase<TodoContext>();
            var todos = TodoFactory.GetTodos();

            var options = await testDb.InitializeAsync(async context =>
            {
                context.Todos.AddRange(todos);
                await context.SaveChangesAsync();
            });

            var queryObject = new QueryObject
            {
                SortBy = "id",
                IsSortAscending = true,
                Page = 0,
                PageSize = 0
            };

            await using (var context = new TodoContext(options))
            {
                var result = await context.Todos.AsQueryable()
                    .ApplyOrdering(queryObject, ColumnsMap)
                    .ApplyPaging(queryObject)
                    .ToListAsync();

                Assert.Equal(5, result.Count);
                Assert.Equal(1, result.First().Id);
                Assert.Equal(5, result.Last().Id);
            }
        }
    }
}