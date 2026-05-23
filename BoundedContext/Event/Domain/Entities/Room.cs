using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Room : EntityBase
{
    public Name Name { get; private set; }
    public int IdTheater { get; private set; }

    public Room(Name name, int idTheater)
    {
        Name = name;
        IdTheater = idTheater;
    }
}