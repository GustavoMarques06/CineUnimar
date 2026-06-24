namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class Password
    {
        protected Password() { }
        public string Value { get; private set; } = null!;
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

            // bcrypt trunca silenciosamente em 72 bytes; senhas maiores seriam equivalentes.
            if (System.Text.Encoding.UTF8.GetByteCount(rawPassword) > 72)
                throw new Exception("Senha não pode ultrapassar 72 caracteres.");

            if (rawPassword.Length < 12)
                throw new Exception("Senha deve ter pelo menos 12 caracteres.");

            if (!rawPassword.Any(char.IsUpper))
                throw new Exception("A senha deve conter ao menos uma letra maiúscula.");

            if (!rawPassword.Any(char.IsDigit))
                throw new Exception("A senha deve conter ao menos um número.");

            if (!rawPassword.Any(c => !char.IsLetterOrDigit(c)))
                throw new Exception("A senha deve conter ao menos um caractere especial.");
        }
    }
}
