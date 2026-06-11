using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases
{
    public class UpdateChairsInEventUseCase
    {
        private readonly IChairsInEventRepository _chairInEventRepository;
        public UpdateChairsInEventUseCase(IChairsInEventRepository chairInEventRepository)
        {
            _chairInEventRepository = chairInEventRepository;

        }
        public async Task RunAsync(UpdateChairInEventRequest updateChairInEvent)
        {
            var chairInEvent = await _chairInEventRepository.GetByIdAsync(updateChairInEvent.Id);

            if (chairInEvent is null)
                throw new ArgumentException("Cadeira não encontrada");



            chairInEvent.UpdateChairInEvent(
                Status: updateChairInEvent.Status,
                IdRoomEvent: updateChairInEvent.IdRoomEvent
            );

            await _chairInEventRepository.UpdateAsync(chairInEvent);
        }
    }
}
