namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateChairRequest
    {
        public Guid Id { get; set; }

        public string ChairPosition { get; set; }

        public Guid IdRoom { get; set; }

        public UpdateChairRequest(Guid id, string chairPosition, Guid idRoom)
        {
            this.Id = id;
            ChairPosition = chairPosition;
            IdRoom = idRoom;
        }
    }
}
