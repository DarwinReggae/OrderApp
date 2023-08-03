using Contracts;
using Microsoft.EntityFrameworkCore;
using OrderApp.Entities;
using Repositories;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<RepositoryContext>(
    o => o.UseNpgsql(builder.Configuration.GetConnectionString("orderappsqlconnection"),
    b => b.MigrationsAssembly("OrderApp")));
builder.Services.AddCors(y => y.AddPolicy("my-policy", y => y.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
builder.Services.AddScoped<IFindFiveNearestBranches, RestaurantBranchService>();
builder.Services.AddScoped<IBaseRepositoryOperations, BaseRepositoryService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();
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
