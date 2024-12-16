using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Mappings;
using NZWalks.API.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

//Inject interface IRegionRepository and concrete implementation SQLRegionRepository
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
//Inject interface IWalkRepository and concrete implementation SQLWalkRepository
builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();

//if we want to switch between different data sources using repository pattern we can use this repository injection instead:
//builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();

//inject Automapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

//add authentication (Microsoft.AspNetCore.Authentication.JwtBearer)
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => 
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"], //it will read from appsetting.json file
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    }); 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//add authorization to the middleware pipeline
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
