using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent
{
    public class GetRoomEventByIdUseCase
    {
        private readonly IRoomEventRepository _roomEventRepository;
        public GetRoomEventByIdUseCase(IRoomEventRepository roomEventRepository)
        {
            _roomEventRepository = roomEventRepository;
        }

        public async Task<Domain.Entities.RoomEvent?> RunAsync(Guid id)
        {
            return await _roomEventRepository.GetByIdAsync(id);
        }
    }
}
