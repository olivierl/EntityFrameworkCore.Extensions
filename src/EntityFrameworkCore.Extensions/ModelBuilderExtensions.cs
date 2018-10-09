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
                entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

                entity.GetProperties()
                    .ToList()
                    .ForEach(p => p.Relational().ColumnName = p.Relational().ColumnName.ToSnakeCase());

                entity.GetKeys()
                    .ToList()
                    .ForEach(k => k.Relational().Name = k.Relational().Name.ToSnakeCase());

                entity.GetForeignKeys()
                    .ToList()
                    .ForEach(k => k.Relational().Name = k.Relational().Name.ToSnakeCase());

                entity.GetIndexes()
                    .ToList()
                    .ForEach(i => i.Relational().Name = i.Relational().Name.ToSnakeCase());
            }
        }
    }
}
