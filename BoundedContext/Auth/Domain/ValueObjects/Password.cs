namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class Password
    {
        protected Password() { }
        public string Value { get; private set; }
        public Password(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("Senha não pode ser vazia.");

            this.Value = value;
        }
        public static void ValidateRaw(string rawPassword)
        {
            if (string.IsNullOrWhiteSpace(rawPassword))
                throw new Exception("Senha não pode ser vazia.");

            if (rawPassword.Length < 6)
                throw new Exception("Senha deve ter pelo menos 6 caracteres.");

            if (!rawPassword.Any(char.IsUpper) || !rawPassword.Any(char.IsDigit))
                throw new Exception("A senha deve conter ao menos uma letra maiúscula e um número.");
        }
    }
}
