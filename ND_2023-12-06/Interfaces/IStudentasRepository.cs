using ND_2023_12_06.Entities;

namespace ND_2023_12_06.Interfaces;

public interface IStudentasRepository
{
    public Task<IEnumerable<Studentas>> ShowStudentai();
    public Task<bool> CreateStudentas(Studentas studentas);
    public Task<bool> EntryExists(Guid StudentasId);
    public Task<bool> UpdateDepartamentas(Guid StudentasId, Guid DepartamentasId);
    public Task<IEnumerable<Studentas>> GetByDepartamentasId(Guid DepartamentasId);
}
