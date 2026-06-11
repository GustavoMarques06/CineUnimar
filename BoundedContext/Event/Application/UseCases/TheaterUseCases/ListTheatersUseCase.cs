using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases
{
    public class ListTheatersUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public ListTheatersUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task<IEnumerable<Theater>> RunAsync()
        {
            return await _theaterRepository.GetAllAsync();
        }
    }
}
