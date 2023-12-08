using ND_2023_12_06.Entities;
using ND_2023_12_06.Exceptions;
using ND_2023_12_06.Interfaces;

namespace ND_2023_12_06.Services;

public class SchoolService : ISchoolService
{
    private readonly IDepartamentasRepository _departamentasRepository;
    private readonly IPaskaitaRepository _paskaitaRepository;
    private readonly IStudentasRepository _studentasRepository;
    private readonly ILogger<SchoolService> _logger;

    public SchoolService(
        IDepartamentasRepository departamentasRepository,
        IPaskaitaRepository paskaitaRepository,
        IStudentasRepository studentasRepository,
        ILogger<SchoolService> logger)
    {
        _departamentasRepository = departamentasRepository;
        _paskaitaRepository = paskaitaRepository;
        _studentasRepository = studentasRepository;
        _logger = logger;
    }

    public async Task<bool> NewPaskaitaToDepartamentas(Paskaita paskaita, Guid DepartamentasId)
    {
        bool departamentasExists = await CheckIfExists<Departamentas>(DepartamentasId);

        if(!departamentasExists) 
        {
            throw new KeyNotFoundException($"Departamentas with DepartamentasId = '{DepartamentasId}' does not exists.");
        }

        return await _departamentasRepository.NewPaskaita(paskaita, DepartamentasId);
    }

    public async Task<bool> NewStudentasToDepartamentas(Studentas studentas, Guid DepartamentasId)
    {
        bool departamentasExists = await CheckIfExists<Departamentas>(DepartamentasId);

        if (!departamentasExists)
        {
            throw new KeyNotFoundException($"Departamentas with DepartamentasId = '{DepartamentasId}' does not exists.");
        }

        return await _departamentasRepository.NewStudentas(studentas, DepartamentasId);
    }

    public async Task<bool> CheckIfExists<T>(Guid Id) where T : class
    {
        if (typeof(T) == typeof(Departamentas))
            return await _departamentasRepository.EntryExists(Id);
        else if (typeof(T) == typeof(Paskaita))
            return await _paskaitaRepository.EntryExists(Id);
        else if (typeof(T) == typeof(Studentas))
            return await _studentasRepository.EntryExists(Id);

        return false;
    }

    public async Task<bool> CreateDepartamentas(Departamentas departamentas)
    {
        return await _departamentasRepository.CreateDepartamentas(departamentas);
    }

    public async Task<bool> CreatePaskaita(Paskaita paskaita)
    {
        return await _paskaitaRepository.CreatePaskaita(paskaita);
    }

    public async Task<bool> CreateStudentas(Studentas studentas)
    {
        return await _studentasRepository.CreateStudentas(studentas);
    }

    public async Task<IEnumerable<Paskaita>> GetPaskaitosByStudentasId(Guid StudentasId)
    {
        bool studentasExists = await CheckIfExists<Studentas>(StudentasId);

        if (!studentasExists)
        {
            throw new KeyNotFoundException($"Studentas with StudentasId = '{StudentasId}' does not exists.");
        }

        return await _paskaitaRepository.GetByStudentasId(StudentasId);
    }

    public async Task<IEnumerable<Paskaita>> GetPaskaitosInDepartamentas(Guid DepartamentasId)
    {
        bool departamentasExists = await CheckIfExists<Departamentas>(DepartamentasId);

        if (!departamentasExists)
        {
            throw new KeyNotFoundException($"Departamentas with DepartamentasId = '{DepartamentasId}' does not exists.");
        }

        return await _paskaitaRepository.GetByDepartamentasId(DepartamentasId);
    }

    public async Task<IEnumerable<Studentas>> GetStudentaiInDepartamentas(Guid DepartamentasId)
    {
        bool departamentasExists = await CheckIfExists<Departamentas>(DepartamentasId);

        if (!departamentasExists)
        {
            throw new KeyNotFoundException($"Departamentas with DepartamentasId = '{DepartamentasId}' does not exists.");
        }

        return await _studentasRepository.GetByDepartamentasId(DepartamentasId);
    }

    public async Task<bool> UpdateDepartamentasForStudentas(Guid StudentasId, Guid DepartamentasId)
    {
        bool studentasExists = await CheckIfExists<Studentas>(StudentasId);
        bool departamentasExists = await CheckIfExists<Departamentas>(DepartamentasId);

        if (!studentasExists)
        {
            throw new KeyNotFoundException($"Studentas with StudentasId = '{StudentasId}' does not exists.");
        }

        if (!departamentasExists)
        {
            throw new KeyNotFoundException($"Departamentas with DepartamentasId = '{DepartamentasId}' does not exists.");
        }

        return await _studentasRepository.UpdateDepartamentas(StudentasId, DepartamentasId);
    }

    public async Task<bool> UpdatePaskaitaForDepartamentas(Guid PaskaitaId, Guid DepartamentasId)
    {
        bool paskaitaExists = await CheckIfExists<Paskaita>(PaskaitaId);
        bool departamentasExists = await CheckIfExists<Departamentas>(DepartamentasId);

        if (!paskaitaExists)
        {
            throw new KeyNotFoundException($"Paskaita with PaskaitaId = '{PaskaitaId}' does not exists.");
        }

        if (!departamentasExists)
        {
            throw new KeyNotFoundException($"Departamentas with DepartamentasId = '{DepartamentasId}' does not exists.");
        }

        return await _departamentasRepository.AddExistingPaskaita(PaskaitaId, DepartamentasId);
    }

    public async Task<IEnumerable<Departamentas>> ShowDepartamentai()
    {
        return await _departamentasRepository.ShowDepartamentai();
    }

    public async Task<IEnumerable<Paskaita>> ShowPaskaitos()
    {
        return await _paskaitaRepository.ShowPaskaitos();
    }

    public async Task<IEnumerable<Studentas>> ShowStudentai()
    {
        return await _studentasRepository.ShowStudentai();
    }
}
