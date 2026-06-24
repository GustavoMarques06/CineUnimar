using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases
{
    public class UpdateEventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public UpdateEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task RunAsync(UpdateEventRequest request)
        {
            if (request is null)
                throw new ArgumentException("Evento não pode ser nulo.");

            var evento = await _eventRepository.GetByIdAsync(request.Id);

            if (evento is null)
                throw new Exception("Evento não encontrado.");

            var description = request.Description is not null ? new Description(request.Description) : null;

            evento.Update(
                new Name(request.Name),
                description,
                request.Date,
                new Duration(request.Duration),
                request.RoomId,
                request.Status,
                request.Price);

            await _eventRepository.UpdateAsync(evento);
        }
    }
}
