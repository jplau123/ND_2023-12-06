using System.ComponentModel.DataAnnotations;

namespace ND_2023_12_06.Entities;

public class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime Created_At {  get; set; }
    public DateTime? Modified_At {  get; set; }
    public bool Is_Deleted { get; set; }
}
