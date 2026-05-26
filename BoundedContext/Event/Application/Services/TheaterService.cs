using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.Entities;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.Services
{
    public class TheaterService
    {
        private readonly ITheaterRepository _theaterRepository;
        public TheaterService(ITheaterRepository theaterRepository) {
            _theaterRepository = theaterRepository;
        }

        public async Task<IEnumerable<Theater>> GetAllAsync()
        {
            return await _theaterRepository.GetAllAsync();
        }

        public async Task<Theater?> GetByIdAsync(Guid id)
        {
            return await _theaterRepository.GetByIdAsync(id);
        }
    }
}
