namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateRoomEventRequest
    {
        public Guid IdRoom { get; private set; }

        public CreateRoomEventRequest(Guid idRoom)
        {
            IdRoom = idRoom;
        }
    }
}
