using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.ChairUseCases
{
    public class UpdateChairUseCase
    {
        private readonly IChairRepository _chairRepository;
        private readonly IRoomRepository _roomRepository;
        public UpdateChairUseCase(IChairRepository chairRepository, IRoomRepository roomRepository)
        {
            _chairRepository = chairRepository;
            _roomRepository = roomRepository;
        }
        public async Task RunAsync(UpdateChairRequest updateChair)
        {
            var chair = await _chairRepository.GetByIdAsync(updateChair.Id);
            var id_room = await _roomRepository.GetByIdAsync(updateChair.IdRoom);

            if (chair is null)
                throw new ArgumentException("Cadeira não encontrada");

            if (id_room is null)
                throw new ArgumentException("Sala não encontrada");


            chair.UpdateChair(
                ChairPosition: new Domain.ValueObjects.ChairPosition(updateChair.ChairPosition),
                IdRoom: updateChair.IdRoom
            );

            await _chairRepository.UpdateAsync(chair);
        }
    }
}
