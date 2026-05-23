using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class ChairsInEvent
{
    public int IdRoomEvent { get; private set; }
    public ChairStatus Status { get; private set; }

    public ChairsInEvent(int idRoomEvent, ChairStatus status)
    {
        IdRoomEvent = idRoomEvent;
        Status = status;
    }
}