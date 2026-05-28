namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;


public class Location
{
    public String value { get; set; }

    public Location(String value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new Exception("O endereço do Evento deve ser inserido");
        }

        this.value = value;


    }
}