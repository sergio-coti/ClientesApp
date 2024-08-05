namespace ClientesApp.API.Models
{
    /// <summary>
    /// Modelo de dados para definir quais campos a API deverá retornar
    /// sempre que precisar gerar uma consulta de dados de cliente
    /// </summary>
    public class ClienteResponseModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public string? Cpf { get; set; }
        public string? Tipo { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}
