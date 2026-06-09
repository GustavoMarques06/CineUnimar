using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class ListRoomsUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public ListRoomsUseCase(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<IEnumerable<Domain.Entities.Room>> RunAsync()
        {
            return await _roomRepository.GetAllAsync();
        }
    }
}
