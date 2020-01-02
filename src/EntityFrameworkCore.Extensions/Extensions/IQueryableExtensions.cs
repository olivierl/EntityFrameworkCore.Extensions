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
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, string sortBy, bool sortDescending,
            Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            if (string.IsNullOrWhiteSpace(sortBy) || !columnsMap.ContainsKey(sortBy))
                return query;

            return sortDescending
                ? query.OrderByDescending(columnsMap[sortBy])
                : query.OrderBy(columnsMap[sortBy]);
        }

        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, ISortable sortableObject,
            Dictionary<string, Expression<Func<T, object>>> columnsMap)
        {
            return ApplyOrdering(query, sortableObject.SortBy, !sortableObject.IsSortAscending, columnsMap);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int page, int pageSize)
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 1000;

            return query
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IPageable pageableObject)
        {
            return ApplyPaging(query, pageableObject.Page, pageableObject.PageSize);
        }
    }
}