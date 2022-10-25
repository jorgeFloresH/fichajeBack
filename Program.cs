using Microsoft.EntityFrameworkCore;
using apiServices.Models;
using System.Text.Json.Serialization;
using apiServices.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<siscolasgamcContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("conexionSQL")));
builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    
    opt.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    opt.JsonSerializerOptions.Converters.Add(new DateTimeJsonConverter());
});

var misReglasCors = "ReglasCors";
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: misReglasCors, builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
}
    );
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(misReglasCors);
app.UseAuthorization();
app.UseWebSockets();
app.MapControllers();

app.Run();