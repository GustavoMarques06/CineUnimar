namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateRoomRequest
    {
        public string Name { get; private set; }
        public Guid IdTheater { get; private set; }

        public CreateRoomRequest(string name, Guid idTheater)
        {
            Name = name;
            IdTheater = idTheater;
        }
    }
}
