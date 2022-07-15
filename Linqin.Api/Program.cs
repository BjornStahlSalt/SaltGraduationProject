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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// var test = Linqin.Api.StringTranslator.ExecuteQuery();
var startCollection = new List<int> { 1, 4, 5, 7, 2, 9, 2, 6, 1 };
RoslynCore.EmitDemo.ExecuteString(".Where(s => s > 1).Where(s => s < 7).OrderBy(s => s).Distinct()", startCollection);

app.Run();
