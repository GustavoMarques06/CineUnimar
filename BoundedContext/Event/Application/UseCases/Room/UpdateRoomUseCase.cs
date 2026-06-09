using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class UpdateRoomUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly IRoomRepository _roomRepository;
        public UpdateRoomUseCase(ITheaterRepository theaterRepository, IRoomRepository roomRepository)
        {
            _theaterRepository = theaterRepository;
            _roomRepository = roomRepository;
        }

        public async Task RunAsync(UpdateRoomRequest updateRoom)
        {
            var theater = await _theaterRepository.GetByIdAsync(updateRoom.IdTheater);

            if (theater is null)
                throw new ArgumentException("Teatro não pode ser nulo");

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
