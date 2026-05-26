using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Chair
{
    public ChairPosition ChairPosition { get; private set; }
    public Guid IdRoom { get; private set; }

    public Chair(ChairPosition chairPosition, Guid idRoom)
    {
        ChairPosition = chairPosition;
        IdRoom = idRoom;
    }
}