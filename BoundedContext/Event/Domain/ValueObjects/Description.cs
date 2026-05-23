namespace Api_Venda_Ingressos.BoundedContext.Event.Domain.ValueObjects;

public class Description
{
    public string Value { get; private set; }
    public Description(string value)
    {
        if (string.IsNullOrEmpty(value))
            throw new Exception("Nome não pode ser vazio");
       
        value = value.Trim();
        
        if(value.Length < 2)
            throw new Exception("Descricrao deve ter pelo menos 2 caracteres");

        this.Value = value;
    }
}