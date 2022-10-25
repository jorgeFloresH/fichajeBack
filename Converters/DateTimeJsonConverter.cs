using System.Text.Json;
using System.Text.Json.Serialization;

namespace apiServices.Converters
{
    public class DateTimeJsonConverter: JsonConverter<DateOnly>
    {

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            return DateOnly.Parse(value!);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("dd/MM/yyyy"));
        }
    }
}
