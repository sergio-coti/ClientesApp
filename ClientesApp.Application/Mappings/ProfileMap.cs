using AutoMapper;
using ClientesApp.Application.Models;
using ClientesApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Mappings
{
    public class ProfileMap : Profile
    {
        public ProfileMap()
        {
            CreateMap<ClienteRequestModel, Cliente>();
            CreateMap<Cliente, ClienteResponseModel>();
        }
    }
}
