namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

public class Price
{
    public Double value { get; set; }

    public Price(Double value)
    {
        if(value < 0)
        {
            throw new Exception("Preço deve ser maior que zero");
        }
        
    }
}