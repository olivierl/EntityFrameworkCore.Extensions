using System.Linq;
using EntityFrameworkCore.Extensions;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    public static class ModelBuilderExtensions
    {
        public static void UseSnakeCaseNamingConvention(this ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                entity.SetTableName(entity.GetTableName()!.ToSnakeCase());

                entity.GetProperties()
                    .ToList()
                    .ForEach(p => p.SetColumnName(p.Name.ToSnakeCase()));

                entity.GetKeys()
                    .ToList()
                    .ForEach(k => k.SetName(k.GetName()!.ToSnakeCase()));

                entity.GetForeignKeys()
                    .ToList()
                    .ForEach(k => k.SetConstraintName(k.GetConstraintName()!.ToSnakeCase()));

                entity.GetIndexes()
                    .ToList()
                    .ForEach(i => i.SetDatabaseName(i.Name!.ToSnakeCase()));
            }
        }
    }
}
