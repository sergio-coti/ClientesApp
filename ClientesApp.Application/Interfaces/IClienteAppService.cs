using ClientesApp.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Interfaces
{
    public interface IClienteAppService : IDisposable
    {
        Task<ClienteResponseModel> AddAsync(ClienteRequestModel request);
        Task<ClienteResponseModel> UpdateAsync(Guid id, ClienteRequestModel request);
        Task<ClienteResponseModel> DeleteAsync(Guid id);
        Task<ClienteResponseModel?> GetByIdAsync(Guid id);
        Task<List<ClienteResponseModel>> GetManyAsync(string nome);
    }
}
