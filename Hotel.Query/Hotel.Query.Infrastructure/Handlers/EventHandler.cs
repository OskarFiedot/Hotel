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
            StartDate = @event.StartDate,
            EndDate = @event.EndDate,
            TotalPrice = @event.TotalPrice,
            DateCreated = @event.DateReserved,
            DateUpdated = @event.DateReserved
        };

        // Option 1 ---------------------------------------------------------
        foreach (Guid roomId in @event.RoomReserved)
        {
            RoomEntity room = await _roomRepository.GetByIdAsync(roomId);

            if (room == null)
            {
                return;
            }

            reservation.Rooms.Add(room);
        }
        // ------------------------------------------------------------------

        await _reservationRepository.CreateAsync(reservation);

        // Option 2
        // foreach (Guid roomId in @event.RoomReserved)
        // {
        //     var roomReserved = new RoomReservedEntity
        //     {
        //         Id = Guid.NewGuid(),
        //         ReservationId = @event.Id,
        //         RoomId = roomId
        //     };

        //     await _roomReservedRepository.CreateAsync(roomReserved);
        // }
    }

    public async Task On(ReservationEditedEvent @event)
    {
        var reservation = await _reservationRepository.GetByIdAsync(@event.Id);

        if (reservation == null)
        {
            return;
        }

        reservation.StartDate = @event.StartDate;
        reservation.EndDate = @event.EndDate;
        reservation.TotalPrice = @event.TotalPrice;
        reservation.DateUpdated = @event.DateEdited;

        List<Guid> currentRoomReserved_Ids = reservation.Rooms.Select(r => r.Id).ToList();

        foreach (Guid roomId in @event.RoomReserved)
        {
            if (!currentRoomReserved_Ids.Contains(roomId))
            {
                RoomEntity room = await _roomRepository.GetByIdAsync(roomId);

                if (room == null)
                {
                    return;
                }

                reservation.Rooms.Add(room);
            }
        }

        foreach (Guid roomId in currentRoomReserved_Ids)
        {
            if (!@event.RoomReserved.Contains(roomId))
            {
                RoomEntity room = await _roomRepository.GetByIdAsync(roomId);

                if (room == null)
                {
                    return;
                }

                reservation.Rooms.Remove(room);
            }
        }

        await _reservationRepository.UpdateAsync(reservation);

        // ------------------------------------------------------------------------------------------------------
        // List<RoomReservedEntity> currentRoomReserved =
        //     await _roomReservedRepository.ListByReservationAsync(@event.Id);

        // if (currentRoomReserved == null || currentRoomReserved.Any(x => x == null))
        // {
        //     return;
        // }

        // List<Guid> currentRoomReserved_Ids = currentRoomReserved.Select(rr => rr.RoomId).ToList();

        // foreach (Guid roomId in @event.RoomReserved)
        // {
        //     if (!currentRoomReserved_Ids.Contains(roomId))
        //     {
        //         var roomReserved = new RoomReservedEntity
        //         {
        //             Id = Guid.NewGuid(),
        //             ReservationId = @event.Id,
        //             RoomId = roomId
        //         };

        //         await _roomReservedRepository.CreateAsync(roomReserved);
        //     }
        // }

        // foreach (RoomReservedEntity roomReserved in currentRoomReserved)
        // {
        //     if (!@event.RoomReserved.Contains(roomReserved.RoomId))
        //     {
        //         await _roomReservedRepository.DeleteAsync(roomReserved.Id);
        //     }
        // }
        // ------------------------------------------------------------------------------------------------------
    }

    public async Task On(ReservationCancelledEvent @event)
    {
        await _reservationRepository.DeleteAsync(@event.Id);

        // List<RoomReservedEntity> roomReservedList =
        //     await _roomReservedRepository.ListByReservationAsync(@event.Id);

        // if (roomReservedList == null || roomReservedList.Any(x => x == null))
        // {
        //     return;
        // }

        // foreach (RoomReservedEntity roomReserved in roomReservedList)
        // {
        //     await _roomReservedRepository.DeleteAsync(roomReserved.Id);
        // }
    }
}
