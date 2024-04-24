using System.ComponentModel.DataAnnotations;
using Pharmacy.Shared.Interfaces;

namespace Pharmacy.Shared.Bases;

public class EntityBase:IEntity
{ [Key]
  
    public int Id { get; set; }
}