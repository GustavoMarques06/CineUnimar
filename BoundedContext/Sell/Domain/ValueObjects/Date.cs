namespace Api_Venda_Ingressos.BoundedContext.Sell.Domain.ValueObjects;

public class Date
{
    public DateTime value { get; set; }

    public Date(DateTime value)
    {
        if(value.Day < 0 || value.Month < 0 || value.Year < 0)
        {
            throw new Exception("A data deve ser valida");
        }

        if(value > DateTime.Today)
        {
            throw new Exception("A data do evento não pode ser no futuro");
        }
    }
}