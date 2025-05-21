using TarefasLibrary.Interface;
using TarefasLibrary.Modelo;
using TarefasLibrary.Repositorio;
using TarefasLibrary.Repositorio.Entity;
using SetorRepositorio = TarefasLibrary.Repositorio.Entity.SetorRepositorio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ReactPolicy",
        policy =>
        {
            policy
              .WithOrigins("http://localhost:53135")   // endereço do seu front
              .AllowAnyHeader()
              .AllowAnyMethod();
        });
});

var _connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string não encontrada");

builder.Services.AddSingleton<ITarefaRepositorio>(new TarefaRepositorio(_connectionString));
builder.Services.AddSingleton<IUsuarioRepositorio>(new UsuarioRepositorio(_connectionString));
builder.Services.AddSingleton<IRepositorio<Empresa>>(new EmpresaRepositorio(_connectionString));
builder.Services.AddSingleton<IRepositorio<Setor>>(new SetorRepositorio(_connectionString));
builder.Services.AddSingleton<IComentarioRepositorio>(new ComentarioRepositorio(_connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

using var context = new AppDbContext(_connectionString);
context.Database.EnsureCreated();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
