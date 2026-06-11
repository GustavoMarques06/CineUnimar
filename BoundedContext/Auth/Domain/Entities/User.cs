using Api_Venda_Ingressos.BoundedContext.Auth.Domain.Enums;
using Api_Venda_Ingressos.BoundedContext.Auth.Domain.ValueObjects;

namespace Api_Venda_Ingressos.BoundedContext.Auth.Domain.Entities
{
    public class User : EntityBase
    {
        

        public Name FirstName { get; private set; } = null!;
        public Name LastName { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public Password PasswordHash { get; private set; } = null!;
        public UserRole Role { get; private set; }
        public CPF CPF { get; private set; } = null!;
        public DateOfBirth DateOfBirth { get; private set; } = null!;

        protected User()
        {
        }

        public User(Name firstName, Name lastName, Email email, DateOfBirth dateOfBirth, CPF cpf, Password passwordHash, UserRole role)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            CPF = cpf;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
        }

        public void UpdateProfile(Name newFirstName, Name newLastName, Email newEmail)
        {
            this.FirstName = newFirstName;
            this.LastName = newLastName;
            this.Email = newEmail;
        }

        public void ChangePassword(Password newPasswordHash)
        {
            this.PasswordHash = newPasswordHash;
        }
    }
}
