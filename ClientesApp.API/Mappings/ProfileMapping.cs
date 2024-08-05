using AutoMapper;
using ClientesApp.API.Models;
using ClientesApp.Data.Entities;
using ClientesApp.Data.Enums;

namespace ClientesApp.API.Mappings
{
    /// <summary>
    /// Classe de configuração para as transferências de dados
    /// realizados pelo AutoMapper (DE/PARA)
    /// </summary>
    public class ProfileMapping : Profile
    {
        //método construtor -> ctor + tab
        public ProfileMapping()
        {
            //Cópia de ClienteRequestModel > Cliente
            CreateMap<ClienteRequestModel, Cliente>()
                .AfterMap((model, entity) => 
                {
                    entity.Id = Guid.NewGuid();
                    entity.Tipo = (TipoCliente) Enum.Parse(typeof(TipoCliente), model.Tipo);
                    entity.DataCadastro = DateTime.Now;
                });

            //Cópia de Cliente > ClienteResponseModel
            CreateMap<Cliente, ClienteResponseModel>()
                .AfterMap((entity, model) =>
                {
                    model.Tipo = entity.Tipo.ToString();
                });
        }
    }
}
