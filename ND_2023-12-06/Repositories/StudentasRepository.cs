using Dapper;
using ND_2023_12_06.Data;
using ND_2023_12_06.Entities;
using ND_2023_12_06.Exceptions;
using ND_2023_12_06.Interfaces;
using Serilog;

namespace ND_2023_12_06.Repositories;

public class StudentasRepository : IStudentasRepository
{
    private readonly IDapperDbContext _context;
    private readonly ILogger<StudentasRepository> _logger;

    public StudentasRepository(IDapperDbContext context, ILogger<StudentasRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> CreateStudentas(Studentas studentas)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "INSERT INTO studentas VALUES (@id, @name)";

            var parameters = new
            {
                id = studentas.Id,
                name = studentas.Vardas
            };


            try
            {
                return await connection.ExecuteAsync(sql, parameters) > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<IEnumerable<Studentas>> ShowStudentai()
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "SELECT id, vardas, departamentas_id, created_at, modified_at FROM studentas";

            try
            {
                return await connection.QueryAsync<Studentas>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<bool> EntryExists(Guid StudentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT COUNT(*) FROM studentas WHERE id = @Id";

            try
            {
                return await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = StudentasId }) > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<bool> UpdateDepartamentas(Guid StudentasId, Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "UPDATE studentas SET departamentas_id = @departamentasId WHERE id = @studentasId";

            var parameters = new
            {
                departamentasId = DepartamentasId,
                studentasId = StudentasId,
            };
            try
            {
                return await connection.ExecuteAsync(query, parameters) > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<IEnumerable<Studentas>> GetByDepartamentasId(Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "SELECT s.id, s.vardas, s.departamentas_id, s.created_at, s.modified_at FROM studentas AS s " +
                "INNER JOIN departamentas AS d ON d.id = s.departamentas_id " +
                "WHERE d.id = @DepartamentasId";

            var parameters = new
            {
                DepartamentasId = DepartamentasId,
            };

            try
            {
                return await connection.QueryAsync<Studentas>(sql, parameters);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }
}
