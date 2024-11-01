using ClientesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.Mappings
{
    /// <summary>
    /// Classe de mapeamento ORM para cliente no EntityFramework
    /// </summary>
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTE"); //nome da tabela

            builder.HasKey(c => c.Id); //chave primária

            //mapeamento do campo 'id'
            builder.Property(c => c.Id)
                .HasColumnName("ID");

            //mapeamento do campo 'nome'
            builder.Property(c => c.Nome)
                .HasColumnName("NOME")
                .HasMaxLength(150)
                .IsRequired();

            //mapeamento do campo 'email'
            builder.Property(c => c.Email)
                .HasColumnName("EMAIL")
                .HasMaxLength(50)
                .IsRequired();

            //mapeamento do campo 'cpf'
            builder.Property(c => c.Cpf)
                .HasColumnName("CPF")
                .HasMaxLength(11)
                .IsRequired();

            //mapeamento do campo 'data de inclusão'
            builder.Property(c => c.DataInclusao)
                .HasColumnName("DATAINCLUSAO")
                .IsRequired();

            //mapeamento do campo 'data de ultima alteração'
            builder.Property(c => c.DataUltimaAlteracao)
                .HasColumnName("DATAULTIMAALTERACAO")
                .IsRequired();

            //mapeamento do campo 'ativo'
            builder.Property(c => c.Ativo)
                .HasColumnName("ATIVO")
                .IsRequired();

            //regra para definir o campo 'email' como único
            builder.HasIndex(c => c.Email).IsUnique();

            //regra para definir o campo 'cpf' como único
            builder.HasIndex(c => c.Cpf).IsUnique();
        }
    }
}
