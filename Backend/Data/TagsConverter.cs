using System.Text.Json;
using System.Text.Json.Serialization;

namespace Backend.Data
{
    public class TagsConverter : JsonConverter<Dictionary<string, int>>
    {
        public override Dictionary<string, int> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Jeśli napotkasz początek tablicy "[", zwróć pusty słownik
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                using (var jsonDoc = JsonDocument.ParseValue(ref reader))
                {
                    return new Dictionary<string, int>();
                }
            }

            // W przeciwnym razie dezerializuj jako normalny słownik
            return JsonSerializer.Deserialize<Dictionary<string, int>>(ref reader, options) ?? new Dictionary<string, int>();
        }

        public override void Write(Utf8JsonWriter writer, Dictionary<string, int> value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }
}
