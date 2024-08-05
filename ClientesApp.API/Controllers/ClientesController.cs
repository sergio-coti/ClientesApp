using AutoMapper;
using ClientesApp.API.Models;
using ClientesApp.Data.Entities;
using ClientesApp.Data.Enums;
using ClientesApp.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Drawing;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //atributos
        private readonly IMapper _mapper;

        //método construtor para injeção de dependência
        public ClientesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Post(ClienteRequestModel model)
        {
            try
            {
                //automapper está copiando os dados da classe Model para Cliente
                var cliente = _mapper.Map<Cliente>(model);

                //gravando o cliente no banco de dados
                var clienteRepository = new ClienteRepository();
                clienteRepository.Inserir(cliente);

                //HTTP 201 - CREATED
                return StatusCode(201, new { mensagem = "Cliente cadastrado com sucesso." });
            }
            catch(Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, ClienteRequestModel model)
        {
            try
            {
                //consultando o cliente no banco de dados através do ID
                var clienteRepository = new ClienteRepository();
                var cliente = clienteRepository.ObterPorId(id);

                //verificar se o cliente foi encontrado
                if(cliente != null)
                {
                    //modificar os dados do cliente
                    cliente.Nome = model.Nome;
                    cliente.Email = model.Email;
                    cliente.Cpf = model.Cpf;
                    cliente.Tipo = (TipoCliente)Enum.Parse(typeof(TipoCliente), model.Tipo);

                    //atualizando no banco de dados
                    clienteRepository.Alterar(cliente);

                    //HTTP 200 -> OK
                    return StatusCode(200, new { mensagem = "Cliente atualizado com sucesso." });
                }
                else
                {
                    //HTTP 400 -> BAD REQUEST
                    return StatusCode(400, new { mensagem = "Cliente não encontrado. Verifique o ID informado." });
                }
            }
            catch(Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                //consultando o cliente no banco de dados através do ID
                var clienteRepository = new ClienteRepository();
                var cliente = clienteRepository.ObterPorId(id);

                //verificando se o cliente foi encontrado
                if(cliente != null)
                {
                    //excluindo o cliente
                    clienteRepository.Excluir(id);

                    //HTTP 200 -> OK
                    return StatusCode(200, new { mensagem = "Cliente excluído com sucesso." });
                }
                else
                {
                    //HTTP 400 -> BAD REQUEST
                    return StatusCode(400, new { mensagem = "Cliente não encontrado. Verifique o ID informado." });
                }
            }
            catch (Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                //instanciando a classe de repositório de banco de dados
                var clienteRepository = new ClienteRepository();
                //executando a consulta de clientes
                var clientes = clienteRepository.ObterTodos();

                //copiando todos os clientes para uma lista de ClienteResponseModel
                var lista = _mapper.Map<List<ClienteResponseModel>>(clientes);

                //HTTP 200 -> OK
                return StatusCode(200, lista);
            }
            catch(Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            try
            {
                //consultando o cliente no banco de dados através do ID
                var clienteRepository = new ClienteRepository();
                var cliente = clienteRepository.ObterPorId(id);

                //verificando se o cliente foi encontrado
                if(cliente != null)
                {
                    //copiando os dados do cliente para a classe ClienteResponseModel
                    var response = _mapper.Map<ClienteResponseModel>(cliente);

                    //HTTP 200 -> OK (SUCESSO)
                    return StatusCode(200, response);
                }
                else
                {
                    //HTTP 204 -> NO CONTENT
                    return StatusCode(204);
                }
            }
            catch(Exception e)
            {
                //HTTP 500 -> INTERNAL SERVER ERROR
                return StatusCode(500, new { mensagem = e.Message });
            }
        }
    }
}
