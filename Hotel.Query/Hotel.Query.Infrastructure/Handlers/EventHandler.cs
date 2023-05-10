using Hotel.Common.Events;
using Hotel.Query.Domain.Entities;
using Hotel.Query.Domain.Repositories;

namespace Hotel.Query.Infrastructure.Handlers;

public class EventHandler : IEventHandler
{
    private readonly IReservationRepository _reservationRepository;
    private readonly ICityRepository _cityRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IHotelRepository _hotelRepository;
    private readonly IRoomRepository _roomRepository;
    private readonly IRoomTypeRepository _roomTypeRepository;
    private readonly IRoomReservedRepository _roomReservedRepository;

    public EventHandler(
        IReservationRepository reservationRepository,
        ICityRepository cityRepository,
        ICountryRepository countryRepository,
        IHotelRepository hotelRepository,
        IRoomRepository roomRepository,
        IRoomTypeRepository roomTypeRepository,
        IRoomReservedRepository roomReservedRepository
    )
    {
        _reservationRepository = reservationRepository;
        _cityRepository = cityRepository;
        _countryRepository = countryRepository;
        _hotelRepository = hotelRepository;
        _roomRepository = roomRepository;
        _roomTypeRepository = roomTypeRepository;
        _roomReservedRepository = roomReservedRepository;
    }

    public async Task On(ReservationCreatedEvent @event)
    {
        var reservation = new ReservationEntity
        {
            Id = @event.Id,
            User = @event.User,
            StartDate = DateOnly.FromDateTime(@event.StartDate),
            EndDate = DateOnly.FromDateTime(@event.EndDate),
            TotalPrice = @event.TotalPrice
        };

        foreach (Guid roomId in @event.RoomReserved)
        {
            reservation.RoomsReserved.Add(
                new RoomReservedEntity { RoomId = roomId, ReservationId = @event.Id }
            );
        }

        await _reservationRepository.CreateAsync(reservation);
    }

    public async Task On(ReservationEditedEvent @event)
    {
        var reservation = await _reservationRepository.GetByIdAsync(@event.Id);

        if (reservation == null)
        {
            return;
        }

        reservation.StartDate = DateOnly.FromDateTime(@event.StartDate);
        reservation.EndDate = DateOnly.FromDateTime(@event.EndDate);
        reservation.TotalPrice = @event.TotalPrice;

        List<Guid> currentRoomReserved_Ids = reservation.RoomsReserved
            .Select(r => r.RoomId)
            .ToList();

        foreach (Guid roomId in @event.RoomReserved)
        {
            if (!currentRoomReserved_Ids.Contains(roomId))
            {
                reservation.RoomsReserved.Add(
                    new RoomReservedEntity { RoomId = roomId, ReservationId = @event.Id }
                );
            }
        }

        foreach (Guid roomId in currentRoomReserved_Ids)
        {
            if (!@event.RoomReserved.Contains(roomId))
            {
                var roomReserved = await _roomReservedRepository.GetByRoomAndReservationAsync(
                    roomId,
                    reservation.Id
                );

                reservation.RoomsReserved.Remove(roomReserved);
            }
        }

        await _reservationRepository.UpdateAsync(reservation);
    }

    public async Task On(ReservationCancelledEvent @event)
    {
        await _reservationRepository.DeleteAsync(@event.Id);
    }
}
