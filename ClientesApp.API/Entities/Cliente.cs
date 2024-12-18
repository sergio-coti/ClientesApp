using ClientesApp.API.Enums;

namespace ClientesApp.API.Entities
{
    /// <summary>
    /// Modelo de entidade para os dados de Cliente
    /// </summary>
    public class Cliente
    {
        #region Propriedades

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public Categoria Categoria { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }
        public bool Ativo { get; set; }

        #endregion
    }
}
