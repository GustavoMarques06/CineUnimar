using Api_Venda_Ingressos.BoundedContext.Event.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Event : EntityBase
{
    public Name Name { get; private set; }
    public Description? Description { get; private set; }
    public DateTime Date { get; private set; }
    public Duration Duration { get; private set; }
    public int RoomId { get; private set; }
    public EventStatus Status { get; private set; }
    public int CategoryId{ get; private set; }
    public int UserCreatorId { get; private set; }

    public Event(Name name, Description? description, DateTime date, Duration duration, int roomId, EventStatus status, int categoryId, int userCreatorId)
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
}