using ND_2023_12_06.Entities;

namespace ND_2023_12_06.Interfaces;

public interface ISchoolService
{
    public Task<bool> CreateDepartamentas(Departamentas departamentas);
    public Task<bool> CreatePaskaita(Paskaita paskaita);
    public Task<bool> CreateStudentas(Studentas studentas);
    public Task<bool> NewStudentasToDepartamentas(Studentas studentas, Guid DepartamentasId);
    public Task<bool> NewPaskaitaToDepartamentas(Paskaita paskaita, Guid DepartamentasId);
    public Task<bool> UpdateDepartamentasForStudentas(Guid StudentasId, Guid DepartamentasId);
    public Task<bool> UpdatePaskaitaForDepartamentas(Guid PaskaitaId, Guid DepartamentasId);
    public Task<IEnumerable<Studentas>> GetStudentaiInDepartamentas(Guid DepartamentasId);
    public Task<IEnumerable<Paskaita>> GetPaskaitosInDepartamentas(Guid DepartamentasId);
    public Task<IEnumerable<Paskaita>> GetPaskaitosByStudentasId(Guid StudentasId);
    public Task<IEnumerable<Departamentas>> ShowDepartamentai();
    public Task<IEnumerable<Paskaita>> ShowPaskaitos();
    public Task<IEnumerable<Studentas>> ShowStudentai();
    public Task<bool> CheckIfExists<T>(Guid Id) where T : class;
}