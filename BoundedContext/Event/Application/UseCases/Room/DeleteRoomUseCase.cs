using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.UseCases.Room
{
    public class DeleteRoomUseCase
    {
        private readonly IRoomRepository _roomRepository;
        public DeleteRoomUseCase(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public async Task RunAsync(Guid id)
        {
            var room = await _roomRepository.GetByIdAsync(id);

            if (room is null)
                throw new ArgumentException("Sala não encontrada");
            
            await _roomRepository.DeleteAsync(room);
        }
    }
}
