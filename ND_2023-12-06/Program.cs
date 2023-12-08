using DbUp;
using ND_2023_12_06.Data;
using ND_2023_12_06.Helpers;
using ND_2023_12_06.Interfaces;
using ND_2023_12_06.Repositories;
using ND_2023_12_06.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDapperDbContext, DapperContext>();
builder.Services.AddSingleton<ResponseHelper>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IDepartamentasRepository, DepartamentasRepository>();
builder.Services.AddScoped<IPaskaitaRepository, PaskaitaRepository>();
builder.Services.AddScoped<IStudentasRepository, StudentasRepository>();

var upgrader = DeployChanges.To
        .PostgresqlDatabase(builder.Configuration.GetConnectionString("school_db"))
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToNowhere()
        .Build();

var result = upgrader.PerformUpgrade();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

var app = builder.Build();

//app.UseExceptionHandler("/Error");

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
