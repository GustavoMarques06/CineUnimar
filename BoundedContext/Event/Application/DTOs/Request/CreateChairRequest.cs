using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateChairRequest
    {

        public string ChairPosition { get; private set; }
        public Guid IdRoom { get; private set; }

        public CreateChairRequest(string chairPosition, Guid idRoom)
        {
            ChairPosition = chairPosition;
            IdRoom = idRoom;
        }
    }
}
