namespace ND_2023_12_06.Entities;

public class DepartamentoPaskaita
{
    public int Departamento_paskaita_id {  get; set; }
    public Guid Departamentas_id { get; set; }
    public Guid Paskaita_id { get; set; }
    public Departamentas? Departamentas { get; set; }
    public Paskaita? Paskaita { get; set; }
}
