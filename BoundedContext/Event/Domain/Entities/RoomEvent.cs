namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class RoomEvent
{
    public int IdRoom { get; private set; }
    public bool IsFull { get; private set; }

    public RoomEvent(int idRoom, bool isFull)
    {
        IdRoom = idRoom;
        IsFull = isFull;
    }
}