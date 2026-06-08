using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request
{
    public class UpdateTheaterRequest
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Location { get; private set; }

        public UpdateTheaterRequest(Guid id, string name, string location)
        {
            Id = id;
            Name = name;
            Location = location;
        }
    }
}
