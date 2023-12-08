using ND_2023_12_06.Interfaces;
using Npgsql;
using System.Data;

namespace ND_2023_12_06.Data;

public class DapperContext : IDapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly string? _connectionString;

    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("school_db");
    }

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}