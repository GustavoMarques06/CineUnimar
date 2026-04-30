namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

public class Quantity
{
    public int value { get; set; }

    public Quantity (int value)
    {
        if(value < 0)
        {
            throw new Exception("A quantidade comprada não pode ser negativa");
        }

    }
}