using QuickKartDataAccessLayer.Models;
using QuickKartDataAccessLayer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DI to use QuickKartDBContext and QuickKartRepository in Controller
builder.Services.AddTransient<QuickKartDBContext>();
builder.Services.AddTransient<QuickKartRepository>(
	c => new QuickKartRepository(c.GetRequiredService<QuickKartDBContext>()));


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
