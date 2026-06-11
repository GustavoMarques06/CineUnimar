using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateEventRequest
    {
        public string Name { get; private set; }
        public Description? Description { get; private set; }
        public DateTime Date { get; private set; }
        public int Duration { get; private set; }
        public Guid RoomId { get; private set; }
        public EventStatus Status { get; private set; }
        public Guid CategoryId { get; private set; }
        public Guid UserCreatorId { get; private set; }
    }
}
