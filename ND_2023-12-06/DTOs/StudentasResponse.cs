﻿using ND_2023_12_06.Entities;

namespace ND_2023_12_06.DTOs;

public class StudentasResponse : BaseEntity
{
    public string? Vardas { get; set; }
    public Guid? Departamentas_id { get; set; }
}
