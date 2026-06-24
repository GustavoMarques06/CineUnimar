using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases
{
    public class CreateChairsInEventUseCase
    {
        private readonly IChairsInEventRepository _chairInEventRepository;
        private readonly IRoomEventRepository _roomEventRepository;

        public CreateChairsInEventUseCase(IChairsInEventRepository chairInEventRepository, IRoomEventRepository roomEventRepository)
        {
            _chairInEventRepository = chairInEventRepository;
            _roomEventRepository = roomEventRepository;
        }

        public async Task<ChairsInEvent> RunAsync(CreateChairInEventRequest createChairInEvent)
        {
            if (createChairInEvent is null)
                throw new ArgumentException("Cadeira não pode ser nulo");

            var roomEvent = await _roomEventRepository.GetByIdAsync(createChairInEvent.IdRoomEvent);
            if (roomEvent is null)
                throw new ArgumentException("Sala de evento não encontrada.");

            var chairInEvent = new ChairsInEvent(createChairInEvent.IdRoomEvent);

            await _chairInEventRepository.SaveAsync(chairInEvent);

            return chairInEvent;
        }
    }
}
