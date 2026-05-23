using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Chair
{
    public ChairPosition ChairPosition { get; private set; }
    public int IdRoom { get; private set; }

    public Chair(ChairPosition chairPosition, int idRoom)
    {
        ChairPosition = chairPosition;
        IdRoom = idRoom;
    }
}