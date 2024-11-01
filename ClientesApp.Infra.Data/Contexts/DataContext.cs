using ClientesApp.Domain.Entities;
using ClientesApp.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.Contexts
{
    /// <summary>
    /// Classe de contexto para conexão com o banco de dados
    /// através do EntityFramework
    /// </summary>
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Criando conexão com o banco de dados do SqlServer
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BDClientes;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //adicionando as classes de mapeamento do projeto
            modelBuilder.ApplyConfiguration(new ClienteMap());
        }
    }
}
