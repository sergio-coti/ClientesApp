using System.ComponentModel.DataAnnotations;

namespace ClientesApp.API.Models
{
    /// <summary>
    /// Modelo de dados para definir quais campos a API deverá receber
    /// para processar os dados de um cliente (cadastro, edição etc).
    /// </summary>
    public class ClienteRequestModel
    {
        [MaxLength(100, ErrorMessage = "Por favor, informe o nome do cliente com no máximo {1} caracteres.")]
        [MinLength(8, ErrorMessage = "Por favor, informe o nome do cliente com pelo menos {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do cliente.")]
        public string? Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do cliente.")]
        public string? Email { get; set; }

        [RegularExpression("^[0-9]{11}$", ErrorMessage = "Por favor informe o cpf com exatamente 11 números.")]
        [Required(ErrorMessage = "Por favor, informe o cpf do cliente.")]
        public string? Cpf { get; set; }

        [RegularExpression("^(Comum|Preferencial|VIP)$", 
            ErrorMessage = "Preencha o tipo com apenas uma das opções: 'Comum', 'Preferencial' ou 'VIP'.")]
        [Required(ErrorMessage = "Por favor, informe o tipo do cliente.")]
        public string? Tipo { get; set; }
    }
}
