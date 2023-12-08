using ND_2023_12_06.DTOs;

namespace ND_2023_12_06.Entities;

public class Departamentas : BaseEntity
{
    public string? Pavadinimas { get; set; }
    public ICollection<Paskaita>? Paskaitos { get; set;}
    public ICollection<Studentas>? Studentai { get; set;}

    public static DepartamentasRequest ConvertToRequest(Departamentas departamentas)
    {
        return new DepartamentasRequest() {
            Pavadinimas = departamentas.Pavadinimas
        };
    }

    public static Departamentas ConvertFromRequest(DepartamentasRequest request)
    {
        return new Departamentas()
        {
            Id = Guid.NewGuid(),
            Pavadinimas = request.Pavadinimas
        };
    }

    public static DepartamentasResponse ConvertToResponse(Departamentas departamentas)
    {
        return new DepartamentasResponse()
        {
            Id = departamentas.Id,
            Pavadinimas = departamentas.Pavadinimas,
            Created_At = departamentas.Created_At,
            Modified_At = departamentas.Modified_At,
            Is_Deleted = departamentas.Is_Deleted
        };
    }

    public static explicit operator Departamentas(Guid v)
    {
        throw new NotImplementedException();
    }
}
