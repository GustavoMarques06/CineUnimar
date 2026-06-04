using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases
{
    public class CreateTheaterUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public CreateTheaterUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task<Theater> RunAsync(Theater theater)
        {
            if (theater is null)
                throw new ArgumentException("Teatro não pode ser nulo");

            await _theaterRepository.SaveAsync(theater);

            return theater;
        }
    }
}
