using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

// ReSharper disable once CheckNamespace
namespace EntityFrameworkCore.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, ISortable sortableObject,
            Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(sortableObject.SortBy) || !columnsMap.ContainsKey(sortableObject.SortBy))
                return query;

            return sortableObject.IsSortAscending
                ? query.OrderBy(columnsMap[sortableObject.SortBy])
                : query.OrderByDescending(columnsMap[sortableObject.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IPageable pageableObject,
            int defaultPageSize = 50)
        {
            if (pageableObject.Page <= 0)
                pageableObject.Page = 1;

            if (pageableObject.PageSize <= 0)
                pageableObject.PageSize = defaultPageSize;

            return query.Skip((pageableObject.Page - 1) * pageableObject.PageSize).Take(pageableObject.PageSize);
        }
    }
}
