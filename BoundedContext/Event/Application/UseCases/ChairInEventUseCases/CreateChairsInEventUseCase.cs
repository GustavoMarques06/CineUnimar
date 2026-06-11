using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases
{
    public class CreateChairsInEventUseCase
    {
        private readonly IChairsInEventRepository _chairInEventRepository;
        private readonly IRoomRepository _roomRepository;

        public CreateChairsInEventUseCase(IChairsInEventRepository chairInEventRepository, IRoomRepository roomRepository)
        {
            _chairInEventRepository = chairInEventRepository;
            _roomRepository = roomRepository;
        }

        public async Task<ChairsInEvent> RunAsync(CreateChairInEventRequest createChairInEvent)
        {
            if (createChairInEvent is null)
                throw new ArgumentException("Cadeira não pode ser nulo");

            


            var chairInEvent = new ChairsInEvent(createChairInEvent.IdRoomEvent);

            await _chairInEventRepository.SaveAsync(chairInEvent);

            return chairInEvent;
        }
    }
}
