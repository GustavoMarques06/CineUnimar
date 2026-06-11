using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateChairInEventRequest
    {
        public Guid IdRoomEvent { get; private set; }
        public ChairStatus Status { get; private set; }

        public CreateChairInEventRequest(Guid idRoomEvent, ChairStatus status)
        {
            IdRoomEvent = idRoomEvent;
            Status = status;
        }
    }
}
