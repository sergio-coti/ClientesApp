using ClientesApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Interface para especialização do repositório de clientes
    /// </summary>
    public interface IClienteRepository : IBaseRepository<Cliente, Guid>
    {

    }
}
