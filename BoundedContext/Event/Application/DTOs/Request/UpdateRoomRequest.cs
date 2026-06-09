using Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateRoomRequest
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid IdTheater { get; private set; }

        public UpdateRoomRequest(Guid id, string name, Guid idTheater)
        {
            Id = id;
            Name = name;
            IdTheater = idTheater;
        }
    }
}
