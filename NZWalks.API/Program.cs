using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. (Inject services)

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject our db context class
//builder.Services collection
//options.UseSqlServer from Microsoft.EntityFrameworkCore, passing the connection string from appsettings.json

builder.Services.AddDbContext<NZWalksDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

//Inject Repository (add scoped which will be the lifetime) - pass both interface and concrete implementation of SQLRegionRepository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

//if we want to switch between different data sources using repository pattern we can use this repository injection instead:
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

//inject Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

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
