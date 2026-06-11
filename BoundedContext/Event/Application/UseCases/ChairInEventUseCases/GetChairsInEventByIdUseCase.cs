using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairInEventUseCases
{
    public class GetChairsInEventByIdUseCase
    {
        private readonly IChairsInEventRepository _chairInEventRepository;
        public GetChairsInEventByIdUseCase(IChairsInEventRepository chairInEventRepository)
        {
            _chairInEventRepository = chairInEventRepository;
        }
        public async Task<ChairsInEvent?> RunAsync(Guid id)
        {
            return await _chairInEventRepository.GetByIdAsync(id);
        }
    }
}
