using ClientesApp.API.Dtos;
using ClientesApp.API.Entities;
using Dapper;
using System.Data.SqlClient;

namespace ClientesApp.API.Repositories
{
    /// <summary>
    /// Repositório para gravar, alterar, excluir e consultar
    /// dados de clientes no banco de dados.
    /// </summary>
    public class ClienteRepository
    {
        //atributo para armazenar a string de conexão do banco de dados
        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ClientesApp;Integrated Security=True;";

        /// <summary>
        /// Método para inserir um registro de cliente
        /// na tabela do banco de dados do SqlServer
        /// </summary>
        public void Inserir(Cliente cliente)
        {
            //abrir conexão com o banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    INSERT INTO CLIENTE(ID, NOME, EMAIL, CPF, CATEGORIA, DATACRIACAO, DATAULTIMAALTERACAO, ATIVO)
                    VALUES(@ID, @NOME, @EMAIL, @CPF, @CATEGORIA, @DATACRIACAO, @DATAULTIMAALTERACAO, @ATIVO)
                ";

                connection.Execute(query, new
                {
                    @ID = cliente.Id,
                    @NOME = cliente.Nome,
                    @EMAIL = cliente.Email,
                    @CPF = cliente.Cpf,
                    @CATEGORIA = (int) cliente.Categoria,
                    @DATACRIACAO = cliente.DataCriacao,
                    @DATAULTIMAALTERACAO = cliente.DataUltimaAlteracao,
                    @ATIVO = cliente.Ativo
                });
            }
        }

        /// <summary>
        /// Método para alterar um registro de cliente
        /// na tabela do banco de dados do SqlServer
        /// </summary>
        public void Atualizar(Cliente cliente)
        {
            //abrir conexão com o banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    UPDATE CLIENTE
                    SET
                        NOME = @NOME,
                        EMAIL = @EMAIL,
                        CPF = @CPF,
                        CATEGORIA = @CATEGORIA,
                        DATAULTIMAALTERACAO = @DATAULTIMAALTERACAO
                    WHERE
                        ID = @ID
                ";

                connection.Execute(query, new
                {
                    @NOME = cliente.Nome,
                    @EMAIL = cliente.Email,
                    @CPF = cliente.Cpf,
                    @CATEGORIA = (int) cliente.Categoria,
                    @DATAULTIMAALTERACAO = DateTime.Now,
                    @ID = cliente.Id
                });
            }
        }

        /// <summary>
        /// Método para excluir um registro de cliente
        /// na tabela do banco de dados do SqlServer
        /// </summary>
        public void Excluir(Guid id)
        {
            //abrir conexão com o banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    UPDATE CLIENTE
                    SET
                        ATIVO = @ATIVO,
                        DATAULTIMAALTERACAO = @DATAULTIMAALTERACAO
                    WHERE
                        ID = @ID
                ";

                connection.Execute(query, new
                {
                    @ATIVO = false, //inativar o cliente
                    @DATAULTIMAALTERACAO = DateTime.Now,
                    @ID = id
                });
            }
        }

        /// <summary>
        /// Método para consultar todos os clientes
        /// na tabela do banco de dados do SqlServer
        /// </summary>
        public List<Cliente> Consultar()
        {
            //abrir conexão com o banco de dados
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT * FROM CLIENTE 
                    WHERE ATIVO = 1
                    ORDER BY NOME
                ";

                return connection
                    .Query<Cliente>(query)
                    .ToList();
            }
        }

        /// <summary>
        /// Método para consultar 1 cliente na tabela[
        /// do banco de dados através do ID informado
        /// </summary>
        public Cliente? ObterPorId(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT * FROM CLIENTE
                    WHERE ID = @ID
                    AND ATIVO = 1
                ";

                return connection
                    .Query<Cliente>(query, new { @ID = id })
                    .FirstOrDefault();
            }
        }

        /// <summary>
        /// Método para consultar os dados que serão utilizados na geração 
        /// do dashboard de clientes, para mostrar a quantidade de clientes
        /// cadastrados no banco de dados para cada categoria.
        /// </summary>
        public List<ClienteDashboardDto> ConsultarDashboard()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = @"
                    SELECT
	                    CASE
		                    WHEN CATEGORIA = 1 THEN 'CLIENTE COMUM'
		                    WHEN CATEGORIA = 2 THEN 'CLIENTE PREFERENCIAL'
		                    WHEN CATEGORIA = 3 THEN 'CLIENTE EMPRESA'
		                    WHEN CATEGORIA = 4 THEN 'CLIENTE VIP'
                        END AS 'NOMECATEGORIA',
	                    COUNT(*) AS 'QUANTIDADECLIENTES'
                        FROM CLIENTE
                        WHERE ATIVO = 1
                        GROUP BY CATEGORIA
                        ORDER BY QUANTIDADECLIENTES DESC
                ";

                return connection.Query<ClienteDashboardDto>(query).ToList();
            }
        }

    }
}
