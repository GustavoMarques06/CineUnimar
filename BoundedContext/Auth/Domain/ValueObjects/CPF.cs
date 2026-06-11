public class CPF
{
    protected CPF() { }
    public string Value { get; } = null!;
    public CPF(string valor)
    {
        if (string.IsNullOrWhiteSpace(valor))
            throw new ArgumentException("CPF não pode ser vazio.");

        var valorLimpo = Limpar(valor);

        if (!Validar(valorLimpo))
            throw new ArgumentException("CPF inválido.");

        Value = valorLimpo;
    }

    private static string Limpar(string valor)
    {
        return new string(valor.Where(char.IsDigit).ToArray());
    }

    private static bool Validar(string cpf)
    {
        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        return true;
    }

    public string Formatar()
    {
        return $"{Value.Substring(0, 3)}.{Value.Substring(3, 3)}.{Value.Substring(6, 3)}-{Value.Substring(9, 2)}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not CPF outro) return false;
        return Value == outro.Value;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value);
    }

    public override string ToString() => Value;
}