using ND_2023_12_06.DTOs;
using ND_2023_12_06.Entities;

namespace ND_2023_12_06.Interfaces;

public interface IDepartamentasRepository
{
    public Task<bool> CreateDepartamentas(Departamentas departamentas);
    public Task<IEnumerable<Departamentas>> ShowDepartamentai();
    public Task<bool> EntryExists(Guid DepartamentasId);
    public Task<bool> NewPaskaita(Paskaita paskaita, Guid DepartamentasId);
    public Task<bool> NewStudentas(Studentas studentas, Guid DepartamentasId);
    public Task<bool> AddExistingPaskaita(Guid PaskaitaId, Guid DepartamentasId);
}
