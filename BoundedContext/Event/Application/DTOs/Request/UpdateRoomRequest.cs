namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateRoomRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid IdTheater { get; set; }
    }
}
