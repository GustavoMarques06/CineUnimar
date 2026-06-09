using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.TheaterUseCases
{
    public class UpdateTheaterUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public UpdateTheaterUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async Task RunAsync(UpdateTheaterRequest updateTheater)
        {
            var theater = await _theaterRepository.GetByIdAsync(updateTheater.Id);

            if (theater is null)
                throw new ArgumentException("Teatro não encontrado");

            theater.UpdateTheater(
                name: new Domain.ValueObjects.Name(updateTheater.Name),
                location: updateTheater.Location
            );

            await _theaterRepository.UpdateAsync(theater);
        }
    }
}
