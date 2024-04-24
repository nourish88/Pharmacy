using Pharmacy.Abstractions.Dtos;

namespace Pharmacy.Application.Features.Medicines.Queries.GetAll;

public class GetAllMedicinesResponse
{
    public List<MedicineViewModel> Medicines { get; set; }
}