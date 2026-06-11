using Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;
using Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.Entities;

public class Theater : EntityBase
{
    public Name Name { get; private set; } = null!;
    public string Location { get; private set; } = null!;

    public Theater(Name name, string location)
    {
        Name = name;
        Location = location;
    }

    private Theater()
    {

    }

    public void UpdateTheater(Name name, string location)
    {
        this.Name = name;
        this.Location = location;
    }
}