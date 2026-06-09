using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class GetRoomByIdUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public GetRoomByIdUseCase(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task<Domain.Entities.Room?> RunAsync(Guid id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }
    }
}
