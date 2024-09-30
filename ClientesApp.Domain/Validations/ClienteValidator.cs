using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Repositories;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Validations
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteValidator(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O Email é obrigatório.")
                .EmailAddress().WithMessage("O Email deve ser um endereço de email válido.")
                .MustAsync(BeUniqueEmail).WithMessage("O Email já está em uso.");

            RuleFor(c => c.Cpf)
                .NotEmpty().WithMessage("O CPF é obrigatório.")
                .Matches(@"^\d{11}$").WithMessage("O CPF deve ter 11 dígitos.")
                .MustAsync(BeUniqueCpf).WithMessage("O CPF já está em uso.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var existingCliente = await _clienteRepository.GetOneAsync(c => c.Email == email);
            return existingCliente == null;
        }

        private async Task<bool> BeUniqueCpf(string cpf, CancellationToken cancellationToken)
        {
            var existingCliente = await _clienteRepository.GetOneAsync(c => c.Cpf == cpf);
            return existingCliente == null;
        }
    }
}