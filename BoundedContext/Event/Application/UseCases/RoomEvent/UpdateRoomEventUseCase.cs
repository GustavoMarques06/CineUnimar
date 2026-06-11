using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent
{
    public class UpdateRoomEventUseCase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IRoomEventRepository _roomEventRepository;
        public UpdateRoomEventUseCase(IRoomRepository roomRepository, IRoomEventRepository roomEventRepository)
        {
            _roomRepository = roomRepository;
            _roomEventRepository = roomEventRepository;
        }

        public async Task RunAsync(UpdateRoomEventRequest updateRoomEvent)
        {
            var room = await _roomRepository.GetByIdAsync(updateRoomEvent.IdRoom);

            if (room is null)
                throw new ArgumentException("Sala não pode ser nulo");

            var roomEvent = await _roomEventRepository.GetByIdAsync(updateRoomEvent.Id);

            if (roomEvent is null)
                throw new ArgumentException("Sala de evento não encontrada");

            roomEvent.UpdateRoom(
               idRoom: updateRoomEvent.Id,
               isFull: updateRoomEvent.IsFull 
            );

            await _roomEventRepository.UpdateAsync(roomEvent);
        }
    }
}
