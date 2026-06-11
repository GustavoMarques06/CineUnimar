using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases
{
    public class DeleteChairsInEventUseCase
    {
        private readonly IChairsInEventRepository _chairInEventRepository;
        public DeleteChairsInEventUseCase(IChairsInEventRepository chairInEventRepository)
        {
            _chairInEventRepository = chairInEventRepository;
        }
        public async Task RunAsync(Guid id)
        {
            var chairInEvent = await _chairInEventRepository.GetByIdAsync(id);

            if (chairInEvent is null)
                throw new ArgumentException("Cadeira não encontrada");

            await _chairInEventRepository.DeleteAsync(chairInEvent);
        }
    }
}
