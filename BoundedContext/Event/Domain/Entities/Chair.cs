using Api_Venda_Ingressos.BoundedContext.Event.Application.DTOs.Request;
using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Chair : EntityBase
{
    public ChairPosition ChairPosition { get; private set; } = null!;
    public Guid IdRoom { get; private set; }

    public Chair(ChairPosition chairPosition, Guid idRoom)
    {
        ChairPosition = chairPosition;
        IdRoom = idRoom;
    }

    private Chair()
    {

    }

    public void UpdateChair(ChairPosition ChairPosition, Guid IdRoom)
    {
        this.ChairPosition = ChairPosition;
        this.IdRoom = IdRoom;
    }
}