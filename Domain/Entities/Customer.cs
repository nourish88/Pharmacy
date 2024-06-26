﻿using Pharmacy.Shared.Bases;

namespace Pharmacy.Domain.Entities;

public class Customer:EntityBase
{

    public string? Name { get; set; }
    public string? SurName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
}