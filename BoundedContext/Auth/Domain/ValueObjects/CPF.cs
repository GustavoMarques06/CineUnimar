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
        if (cpf.Length != 11) return false;
        if (cpf.Distinct().Count() == 1) return false;

        var sum = 0;
        for (var i = 0; i < 9; i++)
            sum += (cpf[i] - '0') * (10 - i);
        var remainder = sum % 11;
        var digit1 = remainder < 2 ? 0 : 11 - remainder;
        if ((cpf[9] - '0') != digit1) return false;

        sum = 0;
        for (var i = 0; i < 10; i++)
            sum += (cpf[i] - '0') * (11 - i);
        remainder = sum % 11;
        var digit2 = remainder < 2 ? 0 : 11 - remainder;
        return (cpf[10] - '0') == digit2;
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