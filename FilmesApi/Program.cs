using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Adiciona o gerador do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura o Swagger UI
if (app.Environment.IsDevelopment())
{
  app.UseSwagger(); // Gera o arquivo swagger.json
  app.UseSwaggerUI(); // Habilita a interface visual em /swagger
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();