using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class CreateRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public CreateRoomUseCase(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Domain.Entities.Room> RunAsync(CreateRoomRequest createRoom)
        {
            if (createRoom is null)
                throw new ArgumentException("Sala não pode ser nulo");

            var room = new Domain.Entities.Room(new Name(createRoom.Name), idTheater: createRoom.IdTheater);

            await _roomRepository.SaveAsync(room);

            return room;
        }
    }
}
