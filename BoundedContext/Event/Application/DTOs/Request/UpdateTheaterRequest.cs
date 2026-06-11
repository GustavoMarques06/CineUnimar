namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateTheaterRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
