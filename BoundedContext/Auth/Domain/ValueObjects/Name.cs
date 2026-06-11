namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class Name
    {
        protected Name() { }
        public string Value { get; private set; } = null!;
        public Name(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new Exception("Nome não pode ser vazio");
            if(value.Length < 2)
                throw new Exception("Nome deve ter pelo menos 2 caracteres");

            value = value.Trim();
            this.Value = value;
        }
    }
}
