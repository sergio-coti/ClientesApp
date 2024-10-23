using ClientesApp.Domain.Dtos;
using ClientesApp.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        //atributos
        private readonly IClienteService _clienteService;

        //construtor para realizar a injeção de dependência
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Serviço para cadastro de clientes
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ClienteResponseDto), 201)]
        public IActionResult Post([FromBody] ClienteRequestDto dto)
        {
            try
            {
                var response = _clienteService.Incluir(dto);
                return StatusCode(201, response);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(400, errors);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço para edição de clientes
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        public IActionResult Put(Guid id, [FromBody] ClienteRequestDto dto)
        {
            try
            {
                var response = _clienteService.Alterar(id, dto);
                return StatusCode(200, response);
            }
            catch (ValidationException e)
            {
                var errors = e.Errors.Select(e => new
                {
                    Name = e.PropertyName,
                    Error = e.ErrorMessage
                });

                return StatusCode(400, errors);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço para exclusão de clientes
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        public IActionResult Delete(Guid id)
        {
            try
            {
                var response = _clienteService.Excluir(id);
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço para consulta de clientes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ClienteResponseDto>), 200)]
        public IActionResult GetAll()
        {
            try
            {
                var response = _clienteService.Consultar();
                
                if(response.Any())
                    return StatusCode(200, response);
                else
                    return StatusCode(204, new { message = "Nenhum cliente encontrado." });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }

        /// <summary>
        /// Serviço para consultar 1 cliente através do ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ClienteResponseDto), 200)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var response = _clienteService.ObterPorId(id);
                return StatusCode(200, response);
            }
            catch (ApplicationException e)
            {
                return StatusCode(422, new { e.Message });
            }
            catch (Exception e)
            {
                return StatusCode(500, new { e.Message });
            }
        }
    }
}
