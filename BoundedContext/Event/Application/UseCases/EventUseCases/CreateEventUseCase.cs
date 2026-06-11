using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases
{
    public class CreateEventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public CreateEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<Events> RunAsync(CreateEventRequest createEvent)
        {
            if (createEvent is null)
                throw new ArgumentException("Cadeira não pode ser nulo");


            var events = new Events(new Name(createEvent.Name), createEvent.Description, createEvent.Date, new Duration(createEvent.Duration), createEvent.RoomId, createEvent.Status, createEvent.CategoryId, createEvent.UserCreatorId);

            await _eventRepository.SaveAsync(events);

            return events;
        }
    }
}
