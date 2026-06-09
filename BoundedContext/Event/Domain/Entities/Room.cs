using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Room : EntityBase 
{
    public Name Name { get; private set; }
    public Guid IdTheater { get; private set; }

    public Room(Name name, Guid idTheater)
    {
        Name = name;
        IdTheater = idTheater;
    }

    public void UpdateRoom(Name name, Guid idTheater)
    {
        Name = name;
        IdTheater = idTheater;
    }
}