using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent
{
    public class CreateRoomEventUseCase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomEventRepository _roomEventRepository;
        public CreateRoomEventUseCase(IRoomRepository roomRepository, IRoomEventRepository roomEventRepository)
        {
            _roomRepository = roomRepository;
            _roomEventRepository = roomEventRepository;
        }

        public async Task<Domain.Entities.RoomEvent> RunAsync(CreateRoomEventRequest createRoom)
        {
            var room = await _roomRepository.GetByIdAsync(createRoom.IdRoom);

            if (room is null)
                throw new ArgumentException("Sala não pode ser nulo");

            if (createRoom is null)
                throw new ArgumentException("Sala do evento não pode ser nulo");

            var roomEvent = new Domain.Entities.RoomEvent(idRoom: createRoom.IdRoom);

            await _roomEventRepository.SaveAsync(roomEvent);

            return roomEvent;
        }
    }
}
