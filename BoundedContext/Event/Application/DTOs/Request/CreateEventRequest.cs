using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateEventRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public Guid RoomId { get; set; }
        public EventStatus Status { get; set; }
        public Guid CategoryId { get; set; }
        public Guid UserCreatorId { get; set; }
    }
}
