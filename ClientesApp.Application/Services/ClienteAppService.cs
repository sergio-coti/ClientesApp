using AutoMapper;
using ClientesApp.Application.Interfaces;
using ClientesApp.Application.Models;
using ClientesApp.Domain.Entities;
using ClientesApp.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteDomainService _clienteDomainService;
        private readonly IMapper _mapper;

        public ClienteAppService(IClienteDomainService clienteDomainService, IMapper mapper)
        {
            _clienteDomainService = clienteDomainService;
            _mapper = mapper;
        }

        public async Task<ClienteResponseModel> AddAsync(ClienteRequestModel request)
        {
            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = Guid.NewGuid();
            var result = await _clienteDomainService.AddAsync(cliente);
            return _mapper.Map<ClienteResponseModel>(result);
        }

        public async Task<ClienteResponseModel> UpdateAsync(Guid id, ClienteRequestModel request)
        {
            var cliente = _mapper.Map<Cliente>(request);
            cliente.Id = id;
            var result = await _clienteDomainService.UpdateAsync(cliente);
            return _mapper.Map<ClienteResponseModel>(result);
        }

        public async Task<ClienteResponseModel> DeleteAsync(Guid id)
        {
            var result = await _clienteDomainService.DeleteAsync(id);
            return _mapper.Map<ClienteResponseModel>(result);
        }

        public async Task<ClienteResponseModel?> GetByIdAsync(Guid id)
        {
            var cliente = await _clienteDomainService.GetByIdAsync(id);
            return _mapper.Map<ClienteResponseModel>(cliente);
        }

        public async Task<List<ClienteResponseModel>> GetManyAsync(string nome)
        {
            var clientes = await _clienteDomainService.GetManyAsync(nome);
            return _mapper.Map<List<ClienteResponseModel>>(clientes);
        }

        public void Dispose()
        {
            _clienteDomainService.Dispose();
        }
    }
}
