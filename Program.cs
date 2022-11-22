using Microsoft.EntityFrameworkCore;
using apiServices.Models;
using System.Text.Json.Serialization;
using apiServices.Converters;
using AutoMapper;
using apiServices.MappingProfiles;
using apiServices.Services;

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

//----------------------------------Paginacion----------------------------------
var automapper = new MapperConfiguration(item => item.AddProfile(new RequestToDomainProfile()));
IMapper mapper = automapper.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddScoped<IAgenciaService, AgenciaService>();
builder.Services.AddScoped<IAgenciaPageService, AgenciaPageService>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioPageService, UsuarioPageService>();

builder.Services.AddScoped<IRequisitoService, RequisitosService>();
builder.Services.AddScoped<IRequisitoPageService, RequisitoPageService>();

builder.Services.AddScoped<ITramiteService, TramiteService>();
builder.Services.AddScoped<ITramitePageService, TramitePageService>();

builder.Services.AddScoped<IVentanillaService, VentanillaService>();
builder.Services.AddScoped<IVentanillaPageService, VentanillaPageService>();
//--------------------------------------------------------------------
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
