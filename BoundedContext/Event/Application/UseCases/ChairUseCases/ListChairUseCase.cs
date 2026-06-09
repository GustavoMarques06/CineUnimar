using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases
{
    public class ListChairUseCase
    {
        private readonly IChairRepository _chairRepository;
        public ListChairUseCase(IChairRepository chairRepository)
        {
            _chairRepository = chairRepository;
        }
        public async Task<IEnumerable<Chair>> RunAsync()
        {
            return await _chairRepository.GetAllAsync();
        }
    }
}
