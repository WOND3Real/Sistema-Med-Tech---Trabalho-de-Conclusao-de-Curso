using Microsoft.EntityFrameworkCore;
using SMT.Models;

var builder = WebApplication.CreateBuilder(args);

// Configura o DbContext para usar o PostgreSQL
builder.Services.AddDbContext<ContextoBD>(options =>options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registra o PacienteService como um serviço
builder.Services.AddScoped<PacienteService>();
builder.Services.AddControllers();

var app = builder.Build();

// Mapeia a rota padrão
app.MapGet("/", () => "Hello World!");

app.Run();
