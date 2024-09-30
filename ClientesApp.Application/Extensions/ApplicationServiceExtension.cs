using ClientesApp.Application.Interfaces;
using ClientesApp.Application.Mappings;
using ClientesApp.Application.Services;
using ClientesApp.Domain.Interfaces.Services;
using ClientesApp.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Application.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IClienteAppService, ClienteAppService>();
            services.AddAutoMapper(map => new ProfileMap());
            return services;
        }
    }
}
