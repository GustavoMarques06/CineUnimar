namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateTheaterRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
