using AutoMapper;
using Bogus;
using ClientesApp.Application.Interfaces;
using ClientesApp.Application.Models;
using ClientesApp.Application.Services;
using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Exceptions;
using ClientesApp.Domain.Interfaces.Services;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ClientesApp.Application.Tests
{
    public class ClienteAppServiceTest
    {
        private readonly Mock<IClienteDomainService> _clienteDomainServiceMock;
        private readonly IMapper _mapper;
        private readonly ClienteAppService _clienteAppService;

        public ClienteAppServiceTest()
        {
            _clienteDomainServiceMock = new Mock<IClienteDomainService>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ClienteRequestModel, Cliente>();
                cfg.CreateMap<Cliente, ClienteResponseModel>();
            });
            _mapper = config.CreateMapper();

            _clienteAppService = new ClienteAppService(_clienteDomainServiceMock.Object, _mapper);
        }

        [Fact(DisplayName = "Adicionar cliente com sucesso")]
        public async Task AddAsync_ShouldAddCliente_WhenValid()
        {
            var request = new Faker<ClienteRequestModel>("pt_BR")
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"))
                .Generate();

            var cliente = _mapper.Map<Cliente>(request);
            _clienteDomainServiceMock.Setup(s => s.AddAsync(cliente)).ReturnsAsync(cliente);

            var result = await _clienteAppService.AddAsync(request);

            result.Should().NotBeNull();
            result.Nome.Should().Be(cliente.Nome);
            result.Email.Should().Be(cliente.Email);
            result.Cpf.Should().Be(cliente.Cpf);

            _clienteDomainServiceMock.Verify(s => s.AddAsync(cliente), Times.Once);
        }

        [Fact(DisplayName = "Atualizar cliente com sucesso")]
        public async Task UpdateAsync_ShouldUpdateCliente_WhenValid()
        {
            // Arrange
            var id = Guid.NewGuid();
            var request = new Faker<ClienteRequestModel>("pt_BR")
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"))
                .Generate();

            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = id;

            _clienteDomainServiceMock.Setup(s => s.UpdateAsync(cliente)).ReturnsAsync(cliente);

            var result = await _clienteAppService.UpdateAsync(id, request);

            result.Should().NotBeNull();
            result.Nome.Should().Be(cliente.Nome);
            result.Email.Should().Be(cliente.Email);
            result.Cpf.Should().Be(cliente.Cpf);

            _clienteDomainServiceMock.Verify(s => s.UpdateAsync(cliente), Times.Once);
        }

        [Fact(DisplayName = "Excluir cliente com sucesso")]
        public async Task DeleteAsync_ShouldDeleteCliente_WhenValid()
        {
            var id = Guid.NewGuid();
            var cliente = new Cliente { Id = id, Nome = "João", Email = "joao@example.com", Cpf = "12345678901" };
            _clienteDomainServiceMock.Setup(s => s.DeleteAsync(id)).ReturnsAsync(cliente);

            var result = await _clienteAppService.DeleteAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(cliente.Id);

            _clienteDomainServiceMock.Verify(s => s.DeleteAsync(id), Times.Once);
        }

        [Fact(DisplayName = "Buscar cliente por ID com sucesso")]
        public async Task GetByIdAsync_ShouldReturnCliente_WhenExists()
        {
            var id = Guid.NewGuid();
            var cliente = new Cliente { Id = id, Nome = "Maria", Email = "maria@example.com", Cpf = "12345678902" };
            _clienteDomainServiceMock.Setup(s => s.GetByIdAsync(id)).ReturnsAsync(cliente);

            var result = await _clienteAppService.GetByIdAsync(id);

            result.Should().NotBeNull();
            result.Id.Should().Be(cliente.Id);
            result.Nome.Should().Be(cliente.Nome);

            _clienteDomainServiceMock.Verify(s => s.GetByIdAsync(id), Times.Once);
        }

        [Fact(DisplayName = "Buscar múltiplos clientes por nome com sucesso")]
        public async Task GetManyAsync_ShouldReturnClientes_WhenExists()
        {
            var nome = "João";
            var clientes = new List<Cliente>
            {
                new Cliente { Id = Guid.NewGuid(), Nome = "João Silva", Email = "joao@example.com", Cpf = "12345678901" },
                new Cliente { Id = Guid.NewGuid(), Nome = "João Pereira", Email = "joao.p@example.com", Cpf = "12345678902" }
            };

            _clienteDomainServiceMock.Setup(s => s.GetManyAsync(nome)).ReturnsAsync(clientes);

            var result = await _clienteAppService.GetManyAsync(nome);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().AllSatisfy(c => c.Nome.Should().Contain(nome));

            _clienteDomainServiceMock.Verify(s => s.GetManyAsync(nome), Times.Once);
        }

        public void Dispose()
        {
            _clienteDomainServiceMock.Object.Dispose();
        }
    }
}