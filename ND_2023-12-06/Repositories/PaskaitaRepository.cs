using Dapper;
using ND_2023_12_06.Data;
using ND_2023_12_06.Entities;
using ND_2023_12_06.Exceptions;
using ND_2023_12_06.Interfaces;

namespace ND_2023_12_06.Repositories;

public class PaskaitaRepository : IPaskaitaRepository
{
    private readonly IDapperDbContext _context;
    private readonly ILogger<PaskaitaRepository> _logger;

    public PaskaitaRepository(IDapperDbContext context, ILogger<PaskaitaRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> CreatePaskaita(Paskaita paskaita)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "INSERT INTO paskaita VALUES (@id, @name)";

            var parameters = new
            {
                id = paskaita.Id,
                name = paskaita.Pavadinimas
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

    public async Task<IEnumerable<Paskaita>> ShowPaskaitos()
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "SELECT id, pavadinimas, created_at, modified_at FROM paskaita";

            try
            {
                return await connection.QueryAsync<Paskaita>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<bool> EntryExists(Guid PaskaitaId)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT COUNT(*) FROM paskaita WHERE id = @Id";

            try
            {
                return await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = PaskaitaId }) > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<IEnumerable<Paskaita>> GetByStudentasId(Guid StudentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "SELECT p.id, p.pavadinimas, p.created_at, p.modified_at FROM paskaita AS p " +
                "INNER JOIN departamento_paskaita AS dp ON p.id = dp.paskaita_id " +
                "INNER JOIN departamentas AS d ON d.id = dp.departamentas_id " +
                "INNER JOIN studentas AS s ON d.id = s.departamentas_id " +
                "WHERE s.id = @StudentasId";

            var parameters = new
            {
                StudentasId = StudentasId,
            };

            try
            {
                return await connection.QueryAsync<Paskaita>(sql, parameters);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<IEnumerable<Paskaita>> GetByDepartamentasId(Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "SELECT p.id, p.pavadinimas, p.created_at, p.modified_at FROM paskaita AS p " +
                "INNER JOIN departamento_paskaita AS dp ON p.id = dp.paskaita_id " +
                "INNER JOIN departamentas AS d ON d.id = dp.departamentas_id " +
                "WHERE d.id = @DepartamentasId";

            var parameters = new
            {
                DepartamentasId = DepartamentasId,
            };

            try
            {
                return await connection.QueryAsync<Paskaita>(sql, parameters);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }
}
