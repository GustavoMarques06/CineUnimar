using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.EventUseCases
{
    public class DeleteEventUseCase
    {
        private readonly IEventRepository _eventRepository;

        public DeleteEventUseCase(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task RunAsync(Guid id)
        {
            var evento = await _eventRepository.GetByIdAsync(id);

            if (evento is null)
                throw new Exception("Evento não encontrado.");

            await _eventRepository.DeleteAsync(evento);
        }
    }
}
