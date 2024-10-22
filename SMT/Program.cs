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

// Registra o PacienteService como um serviço
builder.Services.AddScoped<PacienteService>();
builder.Services.AddControllers();

var app = builder.Build();

// Middleware de tratamento de exceções para desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Mapeia a rota padrão (opcional)
app.MapGet("/", () => "Hello World!");

// Mapeia os controllers
app.MapControllers();

app.Run();

