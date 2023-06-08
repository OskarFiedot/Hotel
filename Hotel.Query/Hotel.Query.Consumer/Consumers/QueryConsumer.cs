using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using CQRS.Core.Consumers;
using System.Text.Json;
using Hotel.Query.Consumer.Converters;
using Hotel.Query.Consumer.Queries;
using CQRS.Core.Queries;
using Hotel.Query.Domain.Entities;

namespace Hotel.Query.Consumer.Consumers;

public class QueryConsumer : IQueryConsumer
{
    private readonly IQueryHandler _queryHandler;

    public QueryConsumer(IQueryHandler queryHandler)
    {
        _queryHandler = queryHandler;
    }

    public void Consume(string queue)
    {
        var factory = new ConnectionFactory
        {
            HostName = Environment.GetEnvironmentVariable("RABBIT_HOST")
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

        var consumer = new EventingBasicConsumer(channel);

        channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
        Console.WriteLine(" [x] Awaiting RPC requests");

        consumer.Received += async (model, ea) =>
        {
            string response = string.Empty;

            var body = ea.Body.ToArray();
            var props = ea.BasicProperties;
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                var message = Encoding.UTF8.GetString(body);
                System.Console.WriteLine($"{message}");

                var options = new JsonSerializerOptions
                {
                    Converters = { new QueryJsonConverter() }
                };

                var query = JsonSerializer.Deserialize<BaseQuery>(message, options);

                switch (query.Type)
                {
                    case nameof(FindAllHotelsQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllHotelsQuery)query),
                            typeof(List<HotelEntity>)
                        );
                        break;
                    case nameof(FindAllCountriesQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllCountriesQuery)query),
                            typeof(List<CountryEntity>)
                        );
                        break;
                    case nameof(FindAllCitiesQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllCitiesQuery)query),
                            typeof(List<CityEntity>)
                        );
                        break;
                    case nameof(FindAllReservationsQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllReservationsQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindAllRoomsQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllRoomsQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindAllRoomsReservationsQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllRoomsReservationsQuery)query),
                            typeof(List<RoomReservedEntity>)
                        );
                        break;
                    case nameof(FindAllRoomTypesQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindAllRoomTypesQuery)query),
                            typeof(List<RoomTypeEntity>)
                        );
                        break;
                    case nameof(FindCitiesByCountryQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindCitiesByCountryQuery)query),
                            typeof(List<CityEntity>)
                        );
                        break;
                    case nameof(FindCityByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindCityByIdQuery)query),
                            typeof(List<CityEntity>)
                        );
                        break;
                    case nameof(FindCountryByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindCountryByIdQuery)query),
                            typeof(List<CountryEntity>)
                        );
                        break;
                    case nameof(FindHotelByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindHotelByIdQuery)query),
                            typeof(List<HotelEntity>)
                        );
                        break;
                    case nameof(FindHotelsByCityQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindHotelsByCityQuery)query),
                            typeof(List<HotelEntity>)
                        );
                        break;
                    case nameof(FindHotelsByCountryQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindHotelsByCountryQuery)query),
                            typeof(List<HotelEntity>)
                        );
                        break;
                    case nameof(FindReservationByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationByIdQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindReservationsByEndDateQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationsByEndDateQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindReservationsByHotelQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationsByHotelQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindReservationsByPriceQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationsByPriceQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindReservationsByRoomQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationsByRoomQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindReservationsByStartDateQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationsByStartDateQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindReservationsByUserQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindReservationsByUserQuery)query),
                            typeof(List<ReservationEntity>)
                        );
                        break;
                    case nameof(FindRoomByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomByIdQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindRoomReservationByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomReservationByIdQuery)query),
                            typeof(List<RoomReservedEntity>)
                        );
                        break;
                    case nameof(FindRoomsByHotelQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomsByHotelQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindRoomsByNumberOfPeopleQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomsByNumberOfPeopleQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindRoomsByPriceQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomsByPriceQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindRoomsByReservationQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomsByReservationQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindRoomsByTypeQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomsByTypeQuery)query),
                            typeof(List<RoomEntity>)
                        );
                        break;
                    case nameof(FindRoomTypeByIdQuery):
                        response = JsonSerializer.Serialize(
                            await _queryHandler.HandleAsync((FindRoomTypeByIdQuery)query),
                            typeof(List<RoomTypeEntity>)
                        );
                        break;
                    default:
                        response = string.Empty;
                        break;
                }

                System.Console.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine($" [.] {e.Message}");
                response = string.Empty;
            }
            finally
            {
                var responseBytes = Encoding.UTF8.GetBytes(response);
                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: props.ReplyTo,
                    basicProperties: replyProps,
                    body: responseBytes
                );
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            }
        };

        Console.WriteLine(" Press [enter] to exit.");
        Console.ReadLine();
    }
}
