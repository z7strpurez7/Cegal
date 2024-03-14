using Microsoft.EntityFrameworkCore;
using PetClinic.Common;
using PetClinic.Mapper;
using PetClinic.Model.Repository;
using PetClinic.Model.Repository.Implementation;
using PetClinic.Model.Repository.Interfaces;
using PetClinic.Validation;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

//AutoMappers
builder.Services.AddAutoMapper(typeof(ClientAutoMapperProfile));
builder.Services.AddAutoMapper(typeof(PetAutoMapperProfile));
builder.Services.AddAutoMapper(typeof(ClinicAutoMapperProfile));
//Validators
builder.Services.AddScoped<ClientValidator>();
builder.Services.AddScoped<PetValidator>();
builder.Services.AddScoped<ClinicValidator>();

//Entity Framework
builder.Services.AddDbContext<PetClinicDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection"))
//options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"))
);
//Services
builder.Services.AddScoped<IPetClinicRepository, PetClinicRepository>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
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

public partial class Program { }
