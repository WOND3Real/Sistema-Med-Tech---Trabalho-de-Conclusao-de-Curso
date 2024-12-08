using Microsoft.EntityFrameworkCore;
using SMT.Models;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Configura o DbContext para usar o PostgreSQL
    builder.Services.AddDbContext<ContextoBD>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
}
catch (Exception ex)
{
    Console.WriteLine($"Erro ao configurar o DbContext: {ex.Message}");
}

// Registro de serviços
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<EspecialidadeService>();
builder.Services.AddScoped<AdministradorService>();
builder.Services.AddScoped<ContadorDataService>();
builder.Services.AddScoped<ConsultaDetalhadaService>();
builder.Services.AddScoped<ConsultaPacienteService>();
builder.Services.AddScoped<ContribuintesService>();
builder.Services.AddScoped<MedicoEspecialidadeService>();
builder.Services.AddScoped<UnidadeService>();
builder.Services.AddScoped<ConsultaService>();
builder.Services.AddControllers();

// Configuração do CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()    // Permite qualquer origem
              .AllowAnyMethod()    // Permite qualquer método (GET, POST, etc.)
              .AllowAnyHeader();   // Permite qualquer cabeçalho
    });
});

var app = builder.Build();

// Middleware de tratamento de exceções para desenvolvimento
app.UseDeveloperExceptionPage();

// Aplica a política de CORS
app.UseCors("AllowAll");

// Mapeia a rota padrão (opcional)
app.MapGet("/", () => "Hello World!");

// Mapeia os controllers
app.MapControllers();

app.Run();
