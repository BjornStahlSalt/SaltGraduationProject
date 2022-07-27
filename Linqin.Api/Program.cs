using Linqin.Api.Models;

var builder = WebApplication.CreateBuilder(args);

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
}
app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins(new string[] { "http://localhost:3000", "https://linqer.herokuapp.com", "https://calm-smoke-042ccb503.1.azurestaticapps.net" }));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


LinqCompiler.CompilerExe.ExecuteString("Distinct();", new List<ShapeModel>
    {
      new ShapeModel { Shape = "Circle", Color = "Red", PriorityValue = 1},
      new ShapeModel { Shape = "Square", Color = "Green", PriorityValue = 3},
      new ShapeModel { Shape = "Square", Color = "Blue", PriorityValue = 3},
      new ShapeModel { Shape = "Triangle", Color = "Green", PriorityValue = 2},
      new ShapeModel { Shape = "Square", Color = "Red", PriorityValue = 3},
      new ShapeModel { Shape = "Triangle", Color = "Green", PriorityValue = 2}
    }
    );

app.Run();
