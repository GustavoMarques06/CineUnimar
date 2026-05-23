namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

public class ChairPosition
{
    
    public string Value { get; private set; }
    public ChairPosition(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new Exception("Posiçao da cadeira não pode ser vazia");
       
        value = value.Trim();
        
        if(value.Length < 2)
            throw new Exception("Nome deve ter pelo menos 2 caracteres");

        this.Value = value;
    }
}