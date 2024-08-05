using ClientesApp.Data.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Data.Repositories
{
    public class ClienteRepository
    {
        #region String de conexão

        private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDClientesApp;Integrated Security=True;";

        #endregion

        public void Inserir(Cliente cliente)
        {
            var sql = @"
                INSERT INTO CLIENTE(ID, NOME, EMAIL, CPF, TIPO, DATACADASTRO)
                VALUES(@Id, @Nome, @Email, @Cpf, @Tipo, @DataCadastro)
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    @Id = cliente.Id,
                    @Nome = cliente.Nome,
                    @Email = cliente.Email,
                    @Cpf = cliente.Cpf,
                    @Tipo = cliente.Tipo.ToString(),
                    @DataCadastro = cliente.DataCadastro
                });
            }
        }

        public void Alterar(Cliente cliente)
        {
            var sql = @"
                UPDATE CLIENTE 
                SET NOME=@Nome, EMAIL=@Email, CPF=@Cpf, TIPO=@Tipo 
                WHERE ID=@Id
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    @Nome = cliente.Nome,
                    @Email = cliente.Email,
                    @Cpf = cliente.Cpf,
                    @Tipo = cliente.Tipo.ToString(),
                    @Id = cliente.Id
                });
            }
        }

        public void Excluir(Guid id)
        {
            var sql = @"
                DELETE FROM CLIENTE
                WHERE ID=@Id
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new
                {
                    @Id = id
                });
            }
        }

        public List<Cliente> ObterTodos()
        {
            var sql = @"
                SELECT ID, NOME, EMAIL, CPF, TIPO, DATACADASTRO
                FROM CLIENTE
                ORDER BY NOME
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Cliente>(sql).ToList();
            }
        }

        public Cliente? ObterPorId(Guid id)
        {
            var sql = @"
                SELECT ID, NOME, EMAIL, CPF, TIPO, DATACADASTRO
                FROM CLIENTE
                WHERE ID=@Id
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Cliente>(sql, new 
                { 
                    Id = id 
                }).FirstOrDefault();
            }
        }
    }
}
