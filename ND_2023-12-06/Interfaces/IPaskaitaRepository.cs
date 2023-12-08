using ND_2023_12_06.Entities;

namespace ND_2023_12_06.Interfaces;

public interface IPaskaitaRepository
{
    public Task<IEnumerable<Paskaita>> ShowPaskaitos();
    public Task<bool> CreatePaskaita(Paskaita paskaita);
    public Task<bool> EntryExists(Guid PaskaitaId);
    public Task<IEnumerable<Paskaita>> GetByStudentasId(Guid StudentasId);
    public Task<IEnumerable<Paskaita>> GetByDepartamentasId(Guid DepartamentasId);

}
