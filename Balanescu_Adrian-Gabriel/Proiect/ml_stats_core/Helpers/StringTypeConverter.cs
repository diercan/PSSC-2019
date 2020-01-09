using System;
using Newtonsoft.Json;

namespace ml_stats_core.Helpers
{
  public class StringTypeConverter : JsonConverter
  {
    public override bool CanRead => true;
    public override bool CanWrite => true;

    public override bool CanConvert(Type objectType)
    {
      return true;
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
      string json = (string)reader.Value;
      var result = JsonConvert.DeserializeObject(json, objectType);
      return result;
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      var json = JsonConvert.SerializeObject(value);
      serializer.Serialize(writer, json);
    }
  }
}