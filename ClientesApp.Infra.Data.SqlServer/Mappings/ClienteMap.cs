using ClientesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Mappings
{
    /// <summary>
    /// Classe de mapeamento para a entidade Cliente (FluentApi)
    /// </summary>
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            //nome da tabela
            builder.ToTable("TB_CLIENTE");

            //chave primária
            builder.HasKey(c => c.Id);

            //mapeamento dos campos da tabela
            builder.Property(c => c.Id).HasColumnName("ID");
            builder.Property(c => c.Nome).HasColumnName("NOME").HasMaxLength(150).IsRequired();
            builder.Property(c => c.Email).HasColumnName("EMAIL").HasMaxLength(100).IsRequired();
            builder.Property(c => c.Cpf).HasColumnName("CPF").HasMaxLength(11).IsRequired();

            //mapeamento de índices
            builder.HasIndex(c => c.Email).IsUnique();
            builder.HasIndex(c => c.Cpf).IsUnique();
        }
    }
}
