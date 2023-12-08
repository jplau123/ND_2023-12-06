using DbUp;
using ND_2023_12_06.Data;
using ND_2023_12_06.Helpers;
using ND_2023_12_06.Interfaces;
using ND_2023_12_06.Middlewares;
using ND_2023_12_06.Repositories;
using ND_2023_12_06.Services;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Dependency injection
builder.Services.AddSingleton<ResponseHelper>();
builder.Services.AddSingleton<IDapperDbContext, DapperContext>();
builder.Services.AddScoped<ISchoolService, SchoolService>();
builder.Services.AddScoped<IDepartamentasRepository, DepartamentasRepository>();
builder.Services.AddScoped<IPaskaitaRepository, PaskaitaRepository>();
builder.Services.AddScoped<IStudentasRepository, StudentasRepository>();

// DbUp
var upgrader = DeployChanges.To
        .PostgresqlDatabase(builder.Configuration.GetConnectionString("school_db"))
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .LogToNowhere()
        .Build();

var result = upgrader.PerformUpgrade();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

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

// My API auth middleware
app.UseAuthMiddleware();

// My Error handler middleware
app.UseErrorMiddleware();

app.Run();
