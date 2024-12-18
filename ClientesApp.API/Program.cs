var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Criando a configura��o do CORS para dar permiss�o ao projeto Angular
builder.Services.AddCors(
    config => config.AddPolicy("DefaultPolicy", builder => {
        builder.WithOrigins("http://localhost:4200") //servidor do angular
            .AllowAnyMethod() //permiss�o para todos os m�todos (POST, PUT, DELETE, GET etc)
            .AllowAnyHeader(); //permiss�o para enviar parametros de cabe�alho das requisi��es
    })
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Aplicando a pol�tica de CORS
app.UseCors("DefaultPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
