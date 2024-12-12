using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;

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
