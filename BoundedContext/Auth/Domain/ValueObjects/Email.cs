namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class Email
    {
        protected Email() { }
        public string Value { get; private set; }
        public Email(string value)
        {
            if (string.IsNullOrEmpty(value) || !value.Contains("@"))
            {
                throw new Exception("Email com formato invalido");
            }

            this.Value = value.Trim();
        }
    }
}
