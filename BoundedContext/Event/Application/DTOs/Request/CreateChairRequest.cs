namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateChairRequest
    {
        public string ChairPosition { get; set; } = string.Empty;
        public Guid IdRoom { get; set; }
    }
}
