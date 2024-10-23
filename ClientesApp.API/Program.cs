using ClientesApp.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(config => { config.LowercaseUrls = true; });
SwaggerConfiguration.AddSwaggerConfiguration(builder.Services);
DependencyInjectionConfiguration.AddDependencyInjection(builder.Services);
var app = builder.Build();

SwaggerConfiguration.UseSwaggerConfiguration(app);
app.UseAuthorization();
app.MapControllers();
app.Run();
