using System.Text.Json;
using System.Text.Json.Serialization;
using CQRS.Core.Commands;
using Hotel.Cmd.Consumer.Commands;

namespace Hotel.Cmd.Consumer.Converters;

public class CommandJsonConverter : JsonConverter<BaseCommand>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert.IsAssignableFrom(typeof(BaseCommand));
    }

    public override BaseCommand Read(
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
            nameof(CreateReservationCommand)
                => JsonSerializer.Deserialize<CreateReservationCommand>(json, options),
            nameof(EditReservationCommand)
                => JsonSerializer.Deserialize<EditReservationCommand>(json, options),
            nameof(CancelReservationCommand)
                => JsonSerializer.Deserialize<CancelReservationCommand>(json, options),
            _ => throw new JsonException($"{typeDiscriminator} is not supported yet!")
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        BaseCommand value,
        JsonSerializerOptions options
    )
    {
        throw new NotImplementedException();
    }
}
