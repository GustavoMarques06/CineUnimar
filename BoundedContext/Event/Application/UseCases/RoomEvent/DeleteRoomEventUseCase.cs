using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.RoomEvent
{
    public class DeleteRoomEventUseCase
    {
        private readonly IRoomEventRepository _roomEventRepository;
        public DeleteRoomEventUseCase(IRoomEventRepository roomEventRepository)
        {
            _roomEventRepository = roomEventRepository;
        }

        public async Task RunAsync(Guid id)
        {
            var roomEvent = await _roomEventRepository.GetByIdAsync(id);

            if (roomEvent is null)
                throw new ArgumentException("Sala de evento não encontrada");
            
            await _roomEventRepository.DeleteAsync(roomEvent);
        }
    }
}
