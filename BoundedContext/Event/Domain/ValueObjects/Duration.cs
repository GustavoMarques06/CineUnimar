namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

public class Duration
{
    public int Value { get; private set; }
    public Duration(int value)
    {
        if (value <= 0)
            throw new Exception("Duraçao não pode ser menor que 0");

        this.Value = value;
    }
}