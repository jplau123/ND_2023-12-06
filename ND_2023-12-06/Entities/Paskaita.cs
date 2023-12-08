using ND_2023_12_06.DTOs;

namespace ND_2023_12_06.Entities;

public class Paskaita : BaseEntity
{
    public string? Pavadinimas { get; set; }
    public ICollection<Departamentas>? Departamentai { get; set;}

    public static PaskaitaRequest ConvertToRequest(Paskaita paskaita)
    {
        return new PaskaitaRequest()
        {
            Pavadinimas = paskaita.Pavadinimas
        };
    }

    public static Paskaita ConvertFromRequest(PaskaitaRequest request)
    {
        return new Paskaita()
        {
            Id = Guid.NewGuid(),
            Pavadinimas = request.Pavadinimas
        };
    }
    
    public static PaskaitaResponse ConvertToResponse(Paskaita paskaita)
    {
        return new PaskaitaResponse()
        {
            Id = paskaita.Id,
            Pavadinimas = paskaita.Pavadinimas,
            Is_Deleted = paskaita.Is_Deleted,
            Created_At = paskaita.Created_At,
            Modified_At = paskaita.Modified_At,
        };
    }
}
