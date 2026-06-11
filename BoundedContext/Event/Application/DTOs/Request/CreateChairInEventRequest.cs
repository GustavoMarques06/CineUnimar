using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateChairInEventRequest
    {
        public Guid IdRoomEvent { get; set; }
        public ChairStatus Status { get; set; }
    }
}
