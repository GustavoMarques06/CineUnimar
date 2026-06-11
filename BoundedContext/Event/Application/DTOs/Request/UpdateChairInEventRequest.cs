using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateChairInEventRequest
    {
        public Guid Id { get; set; }

        public ChairStatus Status { get; set; }

        public Guid IdRoomEvent { get; set; }

    }
}
