using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Events;
using Hotel.Common.Events;

namespace Hotel.Query.Infrastructure.Converters;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
        {
            throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
        }

        if (!doc.RootElement.TryGetProperty("Type", out var type))
        {
            throw new JsonException("Could not detect the Type discriminator property!");
        }

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        return typeDiscriminator switch
        {
            nameof(ReservationCreatedEvent)
                => JsonSerializer.Deserialize<ReservationCreatedEvent>(json, options),
            nameof(ReservationEditedEvent)
                => JsonSerializer.Deserialize<ReservationEditedEvent>(json, options),
            nameof(ReservationCancelledEvent)
                => JsonSerializer.Deserialize<ReservationCancelledEvent>(json, options),
            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        BaseEvent value,
        JsonSerializerOptions options
    )
    {
        throw new NotImplementedException();
    }
}
