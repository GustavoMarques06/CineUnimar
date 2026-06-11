namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateRoomEventRequest
    {
        public Guid Id { get; set; }
        public Guid IdRoom { get; set; }
        public bool IsFull { get; set; }
    }
}
