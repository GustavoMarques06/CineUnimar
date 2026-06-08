using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases
{
    public class CreateTheaterUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public CreateTheaterUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task<Theater> RunAsync(CreateTheaterRequest createTheater)
        {
            if (createTheater is null)
                throw new ArgumentException("Teatro não pode ser nulo");

            var theater = new Theater(new Name(createTheater.Name), createTheater.Location);

            await _theaterRepository.SaveAsync(theater);

            return theater;
        }
    }
}
