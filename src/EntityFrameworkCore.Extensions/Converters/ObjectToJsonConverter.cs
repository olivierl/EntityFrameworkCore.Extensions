using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

// ReSharper disable once CheckNamespace
namespace EntityFrameworkCore.Extensions
{
    public class ObjectToJsonConverter<T> : ValueConverter<T, string>
    {
        public ObjectToJsonConverter()
            : base(
                obj => JsonSerializer.Serialize(obj, SerializerOptions),
                convertFromProviderExpression: json => JsonSerializer.Deserialize<T>(json, SerializerOptions))
        {
        }

        private static JsonSerializerOptions SerializerOptions =>
            new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Converters = {new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)}
            };
    }
}