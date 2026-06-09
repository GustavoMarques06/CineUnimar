using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class UpdateRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public UpdateRoomUseCase(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task RunAsync(UpdateRoomRequest updateRoom)
        {
            var room = await _roomRepository.GetByIdAsync(updateRoom.Id);

            if (room is null)
                throw new ArgumentException("Sala não encontrada");

            room.UpdateRoom(
                name: new Domain.ValueObjects.Name(updateRoom.Name),
                idTheater: updateRoom.IdTheater
            );

            await _roomRepository.UpdateAsync(room);
        }
    }
}
