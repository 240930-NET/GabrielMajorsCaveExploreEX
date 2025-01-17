using Microsoft.EntityFrameworkCore;
using Cave.Data;
using Cave.Models;
using Adventure.api;
using Cave.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure our DBContext and Repos here

//set up DbContext
builder.Services.AddDbContext<RoomContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Room"), b => b.MigrationsAssembly("Adventure.api")));

// Set up dependencies lifecycles
builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<IRoomService, RoomService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();


// Make your app to use it (Map it)
app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers(); // Maps attribute-routed controllers
    });

app.Run();
