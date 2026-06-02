using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases
{
    public class DeleteTheaterUseCase
    {
        private readonly ITheaterRepository _theaterRepository;
        public DeleteTheaterUseCase(ITheaterRepository theaterRepository)
        {
            _theaterRepository = theaterRepository;
        }
        public async void RunAsync(Theater theater)
        {
            if (theater is null)
                throw new ArgumentException("Teatro não pode ser nulo");

            await _theaterRepository.UpdateAsync(theater);
        }
    }
}
