using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookStore.Application.Converter;

public class DateTimeJsonConverter: JsonConverter<DateTime>
{
    private const string Formato = "dd/MM/yyyy";

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString(), Formato, CultureInfo.InvariantCulture);
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Formato));
    }
}
