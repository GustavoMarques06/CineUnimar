namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateRoomEventRequest
    {
        public Guid Id { get; private set; }
        public Guid IdRoom { get; private set; }
        public bool IsFull { get; private set; }

        public UpdateRoomEventRequest(Guid id, Guid idRoom, bool isFull)
        {
            Id = id;
            IdRoom = idRoom;
            IsFull = isFull;
        }
    }
}
