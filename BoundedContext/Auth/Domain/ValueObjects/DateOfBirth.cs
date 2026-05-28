namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects
{
    public class DateOfBirth
    {
        protected DateOfBirth() { }

        public DateOnly Value { get; private set; }
        private DateOfBirth(DateOnly value)
        {
            this.Value = value;
        }

        public static DateOfBirth Create(DateTime data)
        {
            if (data > DateTime.Today)
                throw new Exception("Data de nascimento não pode ser no futuro.");

            var age = DateTime.Today.Year - data.Year;
            if (data.Date > DateTime.Today.AddYears(-age))
                age--;

            if (age < 16)
                throw new Exception("O usuário deve ter pelo menos 16 anos.");

            return new DateOfBirth(DateOnly.FromDateTime(data));
        }

        public int CalcularIdade()
        {
            var hoje = DateOnly.FromDateTime(DateTime.Today);
            var idade = hoje.Year - Value.Year;

            if (Value > hoje.AddYears(-idade)) idade--;
            return idade;
        }

        public override bool Equals(object obj) =>
            obj is DateOfBirth d && d.Value == Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString("dd/MM/yyyy");
    }
}