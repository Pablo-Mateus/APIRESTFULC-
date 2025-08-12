using BarbeariaApiBackend.Services;
using BarbeariaApiBackend.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<PessoaService>(); // <--- REGISTRA O SERVIÇO DE PESSOA

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    // A URL deve ser a URL do seu Blazor Frontend
    options.AddPolicy("AllowBlazorApp", builder => builder.WithOrigins("https://localhost:7045").AllowAnyHeader().AllowAnyMethod()); 
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazorApp"); // Aplica a política CORS
app.UseAuthorization();
app.MapControllers();

app.Run();