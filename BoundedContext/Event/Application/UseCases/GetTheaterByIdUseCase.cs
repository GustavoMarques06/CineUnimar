using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases
{
    public class GetTheaterByIdUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public GetTheaterByIdUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task<Theater?> RunAsync(Guid id)
        {
            return await _theaterRepository.GetByIdAsync(id);
        }
    }
}
