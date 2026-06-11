using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases
{
    public class GetEventByIdUseCase
    {
        private readonly IEventRepository _eventRepository;

        public GetEventByIdUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Events?> RunAsync(Guid id)
        {
            return await _eventRepository.GetByIdAsync(id);
        }
    }
}
