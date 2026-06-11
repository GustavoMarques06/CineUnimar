using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class ChairsInEvent : EntityBase
{
    public Guid IdRoomEvent { get; private set; }
    public ChairStatus Status { get; private set; }

    public ChairsInEvent(Guid idRoomEvent)
    {
        IdRoomEvent = idRoomEvent;
        Status = ChairStatus.Vacant;
    }

    public void UpdateChairInEvent(ChairStatus Status, Guid IdRoomEvent)
    {
        this.Status = Status;
        this.IdRoomEvent = IdRoomEvent;
    }

    public void OccupyChair()
    {
        if (Status == ChairStatus.Occupied)
            throw new InvalidOperationException("A cadeira já está ocupada.");
        Status = ChairStatus.Occupied;
    }

}