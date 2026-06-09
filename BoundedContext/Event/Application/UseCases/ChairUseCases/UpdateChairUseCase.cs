using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases
{
    public class UpdateChairUseCase
    {
        private readonly IChairRepository _chairRepository;
        public UpdateChairUseCase(IChairRepository chairRepository)
        {
            _chairRepository = chairRepository;
        }
        public async Task RunAsync(UpdateChairRequest updateChair)
        {
            var chair = await _chairRepository.GetByIdAsync(updateChair.Id);

            if (chair is null)
                throw new ArgumentException("Teatro não encontrado");

            chair.UpdateChair(
                ChairPosition: new Domain.ValueObjects.ChairPosition(updateChair.ChairPosition),
                IdRoom: updateChair.IdRoom
            );

            await _chairRepository.UpdateAsync(chair);
        }
    }
}
