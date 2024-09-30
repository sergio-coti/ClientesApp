using Xunit;
using System.Threading.Tasks;
using System.Linq;
using Bogus;
using ClientesApp.Domain.Entities;
using ClientesApp.Infra.Data.SqlServer.Contexts;
using ClientesApp.Infra.Data.SqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;

namespace ClientesApp.Infra.Data.SqlServer.Tests
{
    public class ClienteRepositoryTest
    {
        //atributos
        private readonly Faker<Cliente> _fakerCliente;
        private readonly DataContext _dataContext;
        private readonly ClienteRepository _clienteRepository;

        public ClienteRepositoryTest()
        {
            _fakerCliente = new Faker<Cliente>("pt_BR")
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Nome, f => f.Name.FullName())
                .RuleFor(c => c.Email, f => f.Internet.Email())
                .RuleFor(c => c.Cpf, f => f.Random.Replace("###########"));

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ClientesAppTestsDB")
                .Options;

            _dataContext = new DataContext(options);

            _clienteRepository = new ClienteRepository(_dataContext);
        }

        [Fact(DisplayName = "Adicionar cliente com sucesso no repositório.")]
        public async Task AddAsync_ShouldAddCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);

            var clienteCadastrado = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteCadastrado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteCadastrado?.Nome.Should().Be(cliente.Nome);
            clienteCadastrado?.Email.Should().Be(cliente.Email);
            clienteCadastrado?.Cpf.Should().Be(cliente.Cpf);
        }

        [Fact(DisplayName = "Atualizar cliente com sucesso no repositório.")]
        public async Task UpdateAsync_ShouldUpdateCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);

            cliente.Nome = "Novo Nome";
            cliente.Email = "novoemail@example.com";

            await _clienteRepository.UpdateAsync(cliente);

            var clienteAtualizado = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteAtualizado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteAtualizado?.Nome.Should().Be("Novo Nome");
            clienteAtualizado?.Email.Should().Be("novoemail@example.com");
        }

        [Fact(DisplayName = "Excluir cliente com sucesso no repositório.")]
        public async Task DeleteAsync_ShouldRemoveCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);
            await _clienteRepository.DeleteAsync(cliente);

            var clienteRemovido = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteRemovido != null)
                Assert.True(false, "Cliente não excluído.");
        }

        [Fact(DisplayName = "Obter cliente pelo ID com sucesso no repositório.")]
        public async Task GetByIdAsync_ShouldReturnCliente()
        {
            var cliente = _fakerCliente.Generate();

            await _clienteRepository.AddAsync(cliente);

            var clienteEncontrado = await _clienteRepository.GetByIdAsync(cliente.Id);

            if (clienteEncontrado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteEncontrado?.Id.Should().Be(cliente.Id);
        }

        [Fact(DisplayName = "Obter vários clientes com base em uma condição.")]
        public async Task GetManyAsync_ShouldReturnClientes()
        {
            var clientes = _fakerCliente.Generate(5);
            foreach (var cliente in clientes)
            {
                await _clienteRepository.AddAsync(cliente);
            }

            var clientesEncontrados = await _clienteRepository.GetManyAsync(c => c.Nome != null);

            if (clientesEncontrados == null)
                Assert.True(false, "Cliente não encontrado.");

            clientesEncontrados.Count.Should().BeGreaterOrEqualTo(5);
        }

        [Fact(DisplayName = "Obter cliente único com base em uma condição.")]
        public async Task GetOneAsync_ShouldReturnSingleCliente()
        {
            var clientes = _fakerCliente.Generate(3);
            foreach (var cliente in clientes)
            {
                await _clienteRepository.AddAsync(cliente);
            }

            var clienteEmail = clientes[0].Email;
            var clienteEncontrado = await _clienteRepository.GetOneAsync(c => c.Email == clienteEmail);

            if (clienteEncontrado == null)
                Assert.True(false, "Cliente não encontrado.");

            clienteEncontrado?.Email.Should().Be(clienteEmail);
        }
    }
}