using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases
{
    public class ListChairsInEventUseCase
    {
        private readonly IChairsInEventRepository _chairInEventRepository;
        public ListChairsInEventUseCase(IChairsInEventRepository chairInEventRepository)
        {
            _chairInEventRepository = chairInEventRepository;
        }
        public async Task<IEnumerable<ChairsInEvent>> RunAsync()
        {
            return await _chairInEventRepository.GetAllAsync();
        }
    }
}
