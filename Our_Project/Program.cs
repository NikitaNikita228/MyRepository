using BLL.Service;
using DAL.Context;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Serilog;
=======
using FluentValidation;
>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<BlockContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<BlockRepository>();
builder.Services.AddScoped<BlockService>();

<<<<<<< HEAD
Log.Logger = new LoggerConfiguration().MinimumLevel.Information().WriteTo.Console().WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();
builder.Host.UseSerilog();
=======
builder.Services.AddValidatorsFromAssemblyContaining<BLL.Validators.CreateBlockRequestValidator>();

>>>>>>> 18c5275ae41b02c512913ab99542381de82d1364
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();