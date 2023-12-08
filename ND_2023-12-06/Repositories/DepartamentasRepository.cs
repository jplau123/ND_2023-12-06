using Dapper;
using ND_2023_12_06.Entities;
using ND_2023_12_06.Exceptions;
using ND_2023_12_06.Interfaces;
using System.Transactions;
//using System.Transactions;

namespace ND_2023_12_06.Repositories;

public class DepartamentasRepository : IDepartamentasRepository
{
    private readonly IDapperDbContext _context;
    private readonly ILogger<DepartamentasRepository> _logger;

    public DepartamentasRepository(IDapperDbContext context, ILogger<DepartamentasRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Create new Paskaita to existing Departamentas using DepartamentasId
    // return: true if succesful
    public async Task<bool> NewPaskaita(Paskaita paskaita, Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            connection.Open();

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    string sqlInsertPaskaita = "INSERT INTO paskaita (id, pavadinimas) VALUES (@Id, @Name)";

                    var parameters_paskaita = new
                    {
                        Id = paskaita.Id,
                        Name = paskaita.Pavadinimas
                    };

                    int count_paskaita = await connection.ExecuteAsync(sqlInsertPaskaita, parameters_paskaita);

                    if (count_paskaita == 0)
                    {
                        return false;
                    }

                    string sql_insert_join = "INSERT INTO departamento_paskaita (departamentas_id, paskaita_id) VALUES (@DepartamentasId, @PaskaitaId)";

                    var parameters_departamento_paskaita = new
                    {
                        DepartamentasId = DepartamentasId,
                        PaskaitaId = paskaita.Id
                    };

                    int count_departamento_paskaita = await connection.ExecuteAsync(sql_insert_join, parameters_departamento_paskaita);

                    transaction.Commit();

                    if (count_departamento_paskaita == 0)
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                    throw new DatabaseException("Exception during DB operation occured.");
                }

                return true;
            }
        }
    }

    // Create new Studentas to existing Departamentas using DepartamentasId
    // return: true if succesful
    public async Task<bool> NewStudentas(Studentas studentas, Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "INSERT INTO studentas (id, vardas, departamentas_id) VALUES (@Id, @Name, @DepartamentasId)";

            var parameters_studentas = new
            {
                Id = studentas.Id,
                Name = studentas.Vardas,
                DepartamentasId = DepartamentasId
            };

            try
            {
                return await connection.ExecuteAsync(sql, parameters_studentas) > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<bool> CreateDepartamentas(Departamentas departamentas)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "INSERT INTO departamentas (id, pavadinimas) VALUES (@Id, @Name)";

            var parameters = new
            {
                Id = departamentas.Id,
                Name = departamentas.Pavadinimas
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

    public async Task<bool> EntryExists(Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string query = "SELECT COUNT(*) FROM departamentas WHERE id = @Id";

            try
            {
                return await connection.QuerySingleOrDefaultAsync<int>(query, new { Id = DepartamentasId }) > 0;
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<IEnumerable<Departamentas>> ShowDepartamentai()
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "SELECT id, pavadinimas, created_at, modified_at FROM departamentas";
            try
            {
                return await connection.QueryAsync<Departamentas>(sql);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"An exception occured: {ex.Message}");
                throw new DatabaseException("Exception during DB operation occured.");
            }
        }
    }

    public async Task<bool> AddExistingPaskaita(Guid PaskaitaId, Guid DepartamentasId)
    {
        using (var connection = _context.CreateConnection())
        {
            string sql = "INSERT INTO departamento_paskaita (departamentas_id, paskaita_id) VALUES (@DepartamentasId, @PaskaitaId)";

            var parameters = new
            {
                DepartamentasId = DepartamentasId,
                PaskaitaId = PaskaitaId
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
}
