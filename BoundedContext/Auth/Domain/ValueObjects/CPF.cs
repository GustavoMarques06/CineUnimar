using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class CPF
    {
        public string Value { get; set; }
        public CPF(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("CPF é obrigatório.");

            var CpfLimpo = new string(value.Where(char.IsDigit).ToArray());

            if (!IsValid(CpfLimpo))
                throw new Exception("CPF inválido.");

            this.Value = CpfLimpo;
        }

        private static bool IsValid(string cpf)
        {
            if (cpf.Length != 11) 
                return false;

            if (cpf.All(c => c == cpf[0]))
                return false;


            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public string Formatado() =>
            Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");

        public override string ToString() => Value;
    }
}
