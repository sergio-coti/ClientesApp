using ClientesApp.Infra.Data.SqlServer.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesApp.Infra.Data.SqlServer.Extensions
{
    /// <summary>
    /// Classe de extensão para configurarmos as injeções de dependência
    /// do projeto Infra.Data e do EntityFramework
    /// </summary>
    public static class EntityFrameworkExtension
    {
        public static IServiceCollection AddEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ClientesApp");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
