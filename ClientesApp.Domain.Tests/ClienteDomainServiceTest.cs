using Bogus;
using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Exceptions;
using ClientesApp.Domain.Interfaces.Repositories;
using ClientesApp.Domain.Services;
using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace ClientesApp.Domain.Tests
{
    public class ClienteDomainServiceTests
    {
        private readonly Mock<IClienteRepository> _clienteRepositoryMock;
        private readonly Mock<IValidator<Cliente>> _validatorMock;
        private readonly ClienteDomainService _clienteDomainService;

        public ClienteDomainServiceTests()
        {
            _clienteRepositoryMock = new Mock<IClienteRepository>();
            _validatorMock = new Mock<IValidator<Cliente>>();
            _clienteDomainService = new ClienteDomainService(_clienteRepositoryMock.Object, _validatorMock.Object);
        }

        [Fact(DisplayName = "Adicionar cliente com sucesso")]
        public async Task AddAsync_ShouldAddCliente_WhenValid()
        {
            var cliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"))
                .Generate();

            _validatorMock.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _clienteRepositoryMock.Setup(repo => repo.AddAsync(cliente)).Returns(Task.CompletedTask);

            var result = await _clienteDomainService.AddAsync(cliente);

            result.Should().Be(cliente);
            _clienteRepositoryMock.Verify(repo => repo.AddAsync(cliente), Times.Once);
        }

        [Fact(DisplayName = "Adicionar cliente deve falhar na validação")]
        public async Task AddAsync_ShouldThrowValidationException_WhenInvalid()
        {
            var cliente = new Cliente { Nome = "João" };
            var validationErrors = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Email", "O Email é obrigatório.")
            };

            _validatorMock.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationErrors));

            Func<Task> act = async () => await _clienteDomainService.AddAsync(cliente);

            var exception = await act.Should().ThrowAsync<FluentValidation.ValidationException>();

            exception.Which.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("O Email é obrigatório.");
        }

        [Fact(DisplayName = "Atualizar cliente com sucesso")]
        public async Task UpdateAsync_ShouldUpdateCliente_WhenValid()
        {
            var cliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"))
                .Generate();

            _validatorMock.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            _clienteRepositoryMock.Setup(repo => repo.UpdateAsync(cliente)).Returns(Task.CompletedTask);

            var result = await _clienteDomainService.UpdateAsync(cliente);

            result.Should().Be(cliente);
            _clienteRepositoryMock.Verify(repo => repo.UpdateAsync(cliente), Times.Once);
        }

        [Fact(DisplayName = "Atualizar cliente deve falhar na validação")]
        public async Task UpdateAsync_ShouldThrowValidationException_WhenInvalid()
        {
            var cliente = new Cliente { Nome = "João" };
            var validationErrors = new List<FluentValidation.Results.ValidationFailure>
            {
                new FluentValidation.Results.ValidationFailure("Email", "O Email é obrigatório.")
            };

            _validatorMock.Setup(v => v.ValidateAsync(cliente, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult(validationErrors));

            Func<Task> act = async () => await _clienteDomainService.UpdateAsync(cliente);

            var exception = await act.Should().ThrowAsync<FluentValidation.ValidationException>();

            exception.Which.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be("O Email é obrigatório.");
        }

        [Fact(DisplayName = "Excluir cliente deve falhar quando cliente não encontrado")]
        public async Task DeleteAsync_ShouldThrowClienteNotFoundException_WhenClienteNotFound()
        {
            var clienteId = Guid.NewGuid();
            _clienteRepositoryMock.Setup(repo => repo.GetByIdAsync(clienteId)).ReturnsAsync((Cliente)null);

            Func<Task> act = async () => await _clienteDomainService.DeleteAsync(clienteId);

            var exception = await act.Should().ThrowAsync<ClienteNotFoundException>()
                .WithMessage($"Cliente com ID {clienteId} não encontrado.");
        }

        [Fact(DisplayName = "Excluir cliente com sucesso")]
        public async Task DeleteAsync_ShouldDeleteCliente_WhenClienteExists()
        {
            var cliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"))
                .Generate();

            _clienteRepositoryMock.Setup(repo => repo.GetByIdAsync(cliente.Id)).ReturnsAsync(cliente);
            _clienteRepositoryMock.Setup(repo => repo.DeleteAsync(cliente)).Returns(Task.CompletedTask);

            var result = await _clienteDomainService.DeleteAsync(cliente.Id);

            result.Should().Be(cliente);
            _clienteRepositoryMock.Verify(repo => repo.DeleteAsync(cliente), Times.Once);
        }

        [Fact(DisplayName = "Consultar cliente por ID deve retornar cliente existente")]
        public async Task GetByIdAsync_ShouldReturnCliente_WhenClienteExists()
        {
            var cliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"))
                .Generate();

            _clienteRepositoryMock.Setup(repo => repo.GetByIdAsync(cliente.Id)).ReturnsAsync(cliente);

            var result = await _clienteDomainService.GetByIdAsync(cliente.Id);

            result.Should().Be(cliente);
            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(cliente.Id), Times.Once);
        }

        [Fact(DisplayName = "Consultar cliente por ID deve retornar null quando cliente não encontrado")]
        public async Task GetByIdAsync_ShouldReturnNull_WhenClienteNotFound()
        {
            var clienteId = Guid.NewGuid();
            _clienteRepositoryMock.Setup(repo => repo.GetByIdAsync(clienteId)).ReturnsAsync((Cliente)null);

            var result = await _clienteDomainService.GetByIdAsync(clienteId);

            result.Should().BeNull();
            _clienteRepositoryMock.Verify(repo => repo.GetByIdAsync(clienteId), Times.Once);
        }

        [Fact(DisplayName = "Consultar clientes por nome deve retornar lista de clientes")]
        public async Task GetManyAsync_ShouldReturnClientes_WhenClientesExist()
        {
            var nomeBusca = "João";
            var clienteList = new List<Cliente>
            {
                new Cliente { Id = Guid.NewGuid(), Nome = "João da Silva", Email = "joao@example.com", Cpf = "12345678901" },
                new Cliente { Id = Guid.NewGuid(), Nome = "Joãozinho", Email = "joaozinho@example.com", Cpf = "10987654321" }
            };

            _clienteRepositoryMock.Setup(repo => repo.GetManyAsync(It.IsAny<Expression<Func<Cliente, bool>>>()))
                .ReturnsAsync(clienteList);

            var result = await _clienteDomainService.GetManyAsync(nomeBusca);

            result.Should().BeEquivalentTo(clienteList);
            _clienteRepositoryMock.Verify(repo => repo.GetManyAsync(It.IsAny<Expression<Func<Cliente, bool>>>()), Times.Once);
        }
    }
}