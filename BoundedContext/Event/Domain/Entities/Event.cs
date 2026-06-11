using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Events : EntityBase
{
    public Name Name { get; private set; }
    public Description? Description { get; private set; }
    public DateTime Date { get; private set; }
    public Duration Duration { get; private set; }
    public Guid RoomId { get; private set; }
    public EventStatus Status { get; private set; }
    public Guid CategoryId{ get; private set; }
    public Guid UserCreatorId { get; private set; }

    public Events(Name name, Description? description, DateTime date, Duration duration, Guid roomId, EventStatus status, Guid categoryId, Guid userCreatorId)
    {
        Name = name;
        Description = description;
        Date = date;
        Duration = duration;
        RoomId = roomId;
        Status = status;
        CategoryId = categoryId;
        UserCreatorId = userCreatorId;
    }

    private Events()
    {

    }

    internal object Property(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}