using ND_2023_12_06.DTOs;

namespace ND_2023_12_06.Entities;

public class Studentas : BaseEntity
{
    public string? Vardas { get; set; }
    public Guid? Departamentas_id { get; set; }
    public ICollection<Paskaita>? Paskaitos { get; set; }
    public Departamentas? Departamentas { get; set; }

    public static StudentasRequest ConvertToRequest(Studentas studentas)
    {
        return new StudentasRequest()
        {
            Vardas = studentas.Vardas
        };
    }
    
    public static Studentas ConvertFromRequest(StudentasRequest request)
    {
        return new Studentas()
        {
            Id = Guid.NewGuid(),
            Vardas = request.Vardas
        };
    }
    
    public static StudentasResponse ConvertToResponse(Studentas studentas)
    {
        return new StudentasResponse()
        {
            Id= studentas.Id,
            Vardas = studentas.Vardas,
            Departamentas_id = studentas.Departamentas_id,
            Is_Deleted = studentas.Is_Deleted,
            Created_At = studentas.Created_At,
            Modified_At = studentas.Modified_At,
        };
    }
}
