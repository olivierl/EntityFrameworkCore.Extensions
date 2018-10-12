using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace EntityFrameworkCore.Extensions
{
    public class ObjectToJsonConverter<T> : ValueConverter<T, string>
    {
        public ObjectToJsonConverter()
            : base(
                obj => JsonConvert.SerializeObject(obj, SerializerSettings),
                json => JsonConvert.DeserializeObject<T>(json))
        {
        }

        private static JsonSerializerSettings SerializerSettings
        {
            get
            {
                var settings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    DateFormatHandling = DateFormatHandling.IsoDateFormat
                };
                settings.Converters.Add(new StringEnumConverter {CamelCaseText = true});

                return settings;
            }
        }
    }
}