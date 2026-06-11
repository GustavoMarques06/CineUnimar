using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class EventCategory : EntityBase
{
    public Name Name { get; private set; } = null!;
    public Description? Description { get; private set; }

    public EventCategory(Name name, Description? description)
    {
        Name = name;
        Description = description;
    }

    private EventCategory()
    {

    }
}