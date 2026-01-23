using FilmesApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

try
{
  var builder = WebApplication.CreateBuilder(args);

  // Configuração da Connection String
  var connectionString = builder.Configuration.GetConnectionString("FilmeConnection");

  builder.Services.AddDbContext<FilmeContext>(opts =>
    opts.UseMySql(
      connectionString,
      ServerVersion.AutoDetect(connectionString)
    )
  );

  builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

  builder.Services.AddControllers().AddNewtonsoftJson();
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
  });

  var app = builder.Build();

  // Configurações de Middleware
  if (app.Environment.IsDevelopment())
  {
    app.UseSwagger();
    app.UseSwaggerUI();
  }

  app.UseHttpsRedirection();
  app.UseAuthorization();
  app.MapControllers();

  app.Run();
}
catch (System.Reflection.ReflectionTypeLoadException ex)
{
  // Este bloco vai listar exatamente qual DLL está causando o conflito de versão
  if (ex.LoaderExceptions != null)
  {
    foreach (var le in ex.LoaderExceptions)
    {
      Console.WriteLine($"Erro de Carregamento: {le?.Message}");
    }
  }
}
catch (Exception ex)
{
  // Captura qualquer outro erro genérico na inicialização
  Console.WriteLine($"Erro geral: {ex.Message}");
}