namespace ClientesApp.API.Dtos
{
    /// <summary>
    /// Objeto para modelagem dos dados que a API deverá retornar
    /// para realizar uma consulta de dashboard de clientes
    /// </summary>
    public class ClienteDashboardDto
    {
        public string NomeCategoria { get; set; }
        public int QuantidadeClientes { get; set; }
    }
}
