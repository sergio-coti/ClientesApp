using ClientesApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Definição dos métodos abstratos para o repositório de clientes
    /// </summary>
    public interface IClienteRepository
    {
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        List<Cliente> GetAll();
        Cliente GetById(Guid id);
        bool VerifyEmail(string email, Guid clienteId);
        bool VerifyCpf(string cpf, Guid clienteId);
    }
}
