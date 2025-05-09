using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Repositorio.Entity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<ITarefaRepositorio, TarefaRepositorio>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connStr = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada");
    return new TarefaRepositorio(connStr);
});
//builder.Services.AddSingleton<IRepositorio<Usuario>, UsuarioRepositorio>();

builder.Services.AddSingleton<IUsuarioRepositorio<Usuario>>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connStr = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada");
    return new UsuarioRepositorio(connStr);
});

builder.Services.AddSingleton<IComentarioRepositorio, ComentarioRepository>();

builder.Services.AddSingleton<IRepositorio<Empresa>>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connStr = configuration.GetConnectionString("DefaultConnection")
                 ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada");
    return new EmpresaRepositorio(connStr);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
