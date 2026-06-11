using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases
{
    public class GetChairByIdUseCase
    {
        private readonly IChairRepository _chairRepository;
        public GetChairByIdUseCase(IChairRepository chairRepository)
        {
            _chairRepository = chairRepository;
        }
        public async Task<Chair?> RunAsync(Guid id)
        {
            return await _chairRepository.GetByIdAsync(id);
        }
    }
}
