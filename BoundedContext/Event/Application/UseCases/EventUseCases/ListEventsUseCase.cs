using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases
{
    public class ListEventsUseCase
    {
        private readonly IEventRepository _eventRepository;

        public ListEventsUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<Events>> RunAsync()
        {
            return await _eventRepository.GetAllAsync();
        }
    }
}
