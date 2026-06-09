using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class CreateRoomUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        private readonly IRoomRepository _roomRepository;
        public CreateRoomUseCase(ITheaterRepository theaterRepository, IRoomRepository roomRepository)
        {
            _theaterRepository = theaterRepository;
            _roomRepository = roomRepository;
        }

        public async Task<Domain.Entities.Room> RunAsync(CreateRoomRequest createRoom)
        {
            var theater = await _theaterRepository.GetByIdAsync(createRoom.IdTheater);

            if (theater is null)
                throw new ArgumentException("Teatro não pode ser nulo");

            if (createRoom is null)
                throw new ArgumentException("Sala não pode ser nulo");

            var room = new Domain.Entities.Room(new Name(createRoom.Name), idTheater: createRoom.IdTheater);

            await _roomRepository.SaveAsync(room);

            return room;
        }
    }
}
