namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class Email
    {
        protected Email() { }
        public string Value { get; private set; } = null!;
        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Email não pode ser vazio.");

            try
            {
                var addr = new System.Net.Mail.MailAddress(value.Trim());
                Value = addr.Address;
            }
            catch (FormatException)
            {
                throw new Exception("Email com formato inválido.");
            }
        }
    }
}
