using ClientesApp.API.Dtos;
using ClientesApp.API.Entities;
using ClientesApp.API.Enums;
using ClientesApp.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        /// <summary>
        /// Serviço para cadastro de cliente na API
        /// </summary>
        [HttpPost]
        public IActionResult Post(ClienteRequestDto dto)
        {
            //instanciando um objeto da classe Cliente
            var cliente = new Cliente();

            //capturando os dados da requisição
            cliente.Id = Guid.NewGuid();
            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Cpf = dto.Cpf;
            cliente.Categoria = (Categoria) dto.Categoria;
            cliente.DataCriacao = DateTime.Now;
            cliente.DataUltimaAlteracao = DateTime.Now;
            cliente.Ativo = true;

            //cadastrando o cliente no banco de dados
            var clienteRepository = new ClienteRepository();
            clienteRepository.Inserir(cliente);

            return Ok(new { mensagem = "Cliente cadastrado com sucesso!" });
        }

        /// <summary>
        /// Serviço para atualização de cliente na API
        /// </summary>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, ClienteRequestDto dto)
        {
            //instanciando um objeto da classe Cliente
            var cliente = new Cliente();

            //capturando os dados da requisição
            cliente.Id = id;
            cliente.Nome = dto.Nome;
            cliente.Email = dto.Email;
            cliente.Cpf = dto.Cpf;
            cliente.Categoria = (Categoria) dto.Categoria;

            //atualizando o cliente no banco de dados
            var clienteRepository = new ClienteRepository();
            clienteRepository.Atualizar(cliente);

            return Ok(new { mensagem = "Cliente atualizado com sucesso." });
        }

        /// <summary>
        /// Serviço para exclusão de cliente na API
        /// </summary>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            //excluindo o cliente no banco de dados
            var clienteRepository = new ClienteRepository();
            clienteRepository.Excluir(id);

            return Ok(new { mensagem = "Cliente excluído com sucesso." });
        }

        [HttpGet]
        public IActionResult Get()
        {
            //consultando todos os clientes no banco de dados
            var clienteRepository = new ClienteRepository();
            var clientes = clienteRepository.Consultar();

            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            //consultar o cliente no banco de dados através do ID
            var clienteRepository = new ClienteRepository();
            var cliente = clienteRepository.ObterPorId(id);

            return Ok(cliente);
        }

        [HttpGet]
        [Route("dashboard")]
        public IActionResult GetDashboard()
        {
            //consultar os dados do dashboard de clientes
            var clienteRepository = new ClienteRepository();
            var dados = clienteRepository.ConsultarDashboard();

            return Ok(dados);
        }
    }
}
