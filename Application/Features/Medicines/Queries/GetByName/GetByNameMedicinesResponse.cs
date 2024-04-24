using Pharmacy.Abstractions.Dtos;

namespace Pharmacy.Application.Features.Medicines.Queries.GetByName;

public class GetByNameMedicinesResponse
{
    public List<MedicineViewModel> Medicines { get; set; }

}