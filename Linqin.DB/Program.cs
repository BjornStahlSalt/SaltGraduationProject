using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<LevelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LevelContext") ?? throw new InvalidOperationException("Connection string 'LevelContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(new string[] { "http://localhost:3000", "https://linqer.herokuapp.com" }));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
