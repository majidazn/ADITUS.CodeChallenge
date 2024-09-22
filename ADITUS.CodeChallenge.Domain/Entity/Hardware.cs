using ADITUS.CodeChallenge.Domain.Enum;

namespace ADITUS.CodeChallenge.Domain.Entity
{
  public class Hardware
  {
    public Guid Id { get; init; } 
    public int Quantity  { get; set; }
    
    public int ReservedQuantity  { get; set; }
    public string Name { get; init; }
  }
}