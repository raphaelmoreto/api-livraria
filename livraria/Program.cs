using System.Runtime.Intrinsics.X86;
using Database;
using Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UMA NOVA INST�NCIA DE "DatabaseConnection" � CRIADA PARA CADA REQUISI��O HTTP
builder.Services.AddScoped<DatabaseConnection>();

builder.Services.AddScoped<IAutorRepository, Repositorys.AutorRepository>();

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
