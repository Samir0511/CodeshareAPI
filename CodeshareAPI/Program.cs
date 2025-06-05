// Fix for CS0311: Ensure 'CodeshareDbContext' inherits from 'Microsoft.EntityFrameworkCore.DbContext'  

using DomainLayer.Interface;
using Infrastructure.DbContext;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

// Add services to the container.  
var builder = WebApplication.CreateBuilder(args);

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// ... other service configurations ...


// Enable CORS - add this before other middleware

// ... other middleware configurations ...
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataShare, DataShare>();


builder.Services.AddDbContext<CodeshareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Con")));

var app = builder.Build();
app.UseCors("AllowAngularApp");

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
