using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class ChairsInEvent : EntityBase
{
    public Guid IdRoomEvent { get; private set; }
    public ChairStatus Status { get; private set; }

    public ChairsInEvent(Guid idRoomEvent, ChairStatus status)
    {
        IdRoomEvent = idRoomEvent;
        Status = status;
    }

    public void UpdateChairInEvent(ChairStatus Status, Guid IdRoomEvent)
    {
        this.Status = Status;
        this.IdRoomEvent = IdRoomEvent;
    }
}