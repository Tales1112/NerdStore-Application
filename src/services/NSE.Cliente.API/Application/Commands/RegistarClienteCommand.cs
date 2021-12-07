using NSE.Core.Messages;
using System;

namespace NSE.Cliente.API.Application.Commands
{
    public class RegistarClienteCommand : Command
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Cpf { get; private set; }
        public RegistarClienteCommand(Guid id, string nome, string email, string cpf)
        {
            AggregatedId = id;
            Id = id;
            Nome = nome;
            Email = email;
            Cpf = cpf;
        }

        public override bool EhValido()
        {
            ValidationResult = new RegistrarClienteValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
