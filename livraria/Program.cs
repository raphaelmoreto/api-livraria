using Database;
using Service.InterfaceAutor;
using Repository.InterfaceAutor;
//using Service.InterfaceLivro;
using Repository.InterfaceLivro;
using Database.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// UMA NOVA INSTÂNCIA DE "IDatabaseConnection" É CRIADA PARA CADA REQUISIÇÃO HTTP
builder.Services.AddScoped<IDatabaseConnection, DatabaseConnection>();

// INSERIR OS SERVIÇOS DO "AutoMapper"
//builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IAutorService, Services.AutorService>();
builder.Services.AddScoped<IAutorRepository, Repositorys.AutorRepository>();
//builder.Services.AddScoped<ILivroService, Services.LivroService>();
builder.Services.AddScoped<ILivroRepository, Repositorys.LivroRepository>();

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
