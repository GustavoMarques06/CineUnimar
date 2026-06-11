namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class CreateTheaterRequest
    {
        public string Name { get; private set; }
        public string Location { get; private set; }

        public CreateTheaterRequest(string name, string location)
        {
            Name = name;
            Location = location;
        }
    }
}
