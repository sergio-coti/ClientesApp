namespace ClientesApp.API.Extensions
{
    /// <summary>
    /// Classe de extensão para configuração do Swagger
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Método de extensão para configurações do Swagger na API
        /// </summary>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            // Configurações adicionais do Swagger/OpenAPI
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Clientes API",
                    Version = "v1",
                    Description = "API para gerenciamento de clientes e planos",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Coti Informática",
                        Email = "contato@cotiinformatica.com",
                        Url = new Uri("https://www.cotiinformatica.com")
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
            });

            return services;
        }

        /// <summary>
        /// Método de extensão para executar as configurações do Swagger na API
        /// </summary>
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clientes API v1");
            });

            return app;
        }
    }
}