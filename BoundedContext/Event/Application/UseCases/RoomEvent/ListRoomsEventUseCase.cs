using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent
{
    public class ListRoomsEventUseCase
    {
        private readonly IRoomEventRepository _roomEventRepository;
        public ListRoomsEventUseCase(IRoomEventRepository roomEventRepository)
        {
            _roomEventRepository = roomEventRepository;
        }

        public async Task<IEnumerable<Domain.Entities.RoomEvent>> RunAsync()
        {
            return await _roomEventRepository.GetAllAsync();
        }
    }
}
