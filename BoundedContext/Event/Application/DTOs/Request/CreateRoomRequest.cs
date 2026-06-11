namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateRoomRequest
    {
        public string Name { get; set; } = string.Empty;
        public Guid IdTheater { get; set; }
    }
}
