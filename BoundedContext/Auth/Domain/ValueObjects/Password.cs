namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class Password
    {
        public string Value { get; private set; }
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Senha não pode ser vazia.");
            if (value.Length < 6)
                throw new Exception("Senha deve ter pelo menos 6 caracteres.");

            if (!value.Any(char.IsUpper) || !value.Any(char.IsDigit))
                throw new Exception("A senha deve conter ao menos uma letra maiúscula e um número.");

            this.Value = value;
        }
    }
}
