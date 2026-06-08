namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class RoomEvent : EntityBase
{
    public Guid IdRoom { get; private set; }
    public bool IsFull { get; private set; }

    public RoomEvent(Guid idRoom, bool isFull)
    {
        IdRoom = idRoom;
        IsFull = isFull;
    }
}