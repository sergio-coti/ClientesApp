using System.ComponentModel.DataAnnotations;

namespace ClientesApp.API.Dtos
{
    /// <summary>
    /// Objeto para modelagem dos dados que a API deverá receber
    /// para realizar o cadastro ou edição de um cliente
    /// </summary>
    public class ClienteRequestDto
    {
        #region Propriedades

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do cliente.")]
        public string Nome { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um endereço de email válido.")]
        [Required(ErrorMessage = "Por favor, informe o email do cliente.")]
        public string Email { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "Por favor, informe o cpf com apenas 11 números.")]
        [Required(ErrorMessage = "Por favor, informe o cpf do cliente.")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Por favor, informe a categoria do cliente.")]
        [Range(1, 4, ErrorMessage = "A categoria só deve aceitar valores 1, 2, 3 ou 4.")]
        public int Categoria { get; set; }

        #endregion
    }
}
