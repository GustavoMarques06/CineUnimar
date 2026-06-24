namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

public class Date
{
    public DateTime value { get; set; }

    public Date(DateTime value)
    {
        if(value == default)
            throw new Exception("A data deve ser válida.");

        if(value > DateTime.UtcNow)
            throw new Exception("A data da compra não pode ser colocada para o futuro.");

        this.value = value;
    }
}