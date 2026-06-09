using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Event.Infrastructure.Repository;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases
{
    public class CreateChairUseCase
    {
        private readonly IChairRepository _chairRepository;

        public CreateChairUseCase(IChairRepository chairRepository)
        {
            _chairRepository = chairRepository;
        }

        public async Task<Chair> RunAsync(CreateChairRequest createChair)
        {
            if (createChair is null)
                throw new ArgumentException("Cadeira não pode ser nulo");

            var chair = new Chair(new ChairPosition(createChair.ChairPosition), createChair.IdRoom);

            await _chairRepository.SaveAsync(chair);

            return chair;
        }
    }
}
