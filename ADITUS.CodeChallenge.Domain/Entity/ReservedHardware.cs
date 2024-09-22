namespace ADITUS.CodeChallenge.Domain.Entity
{
  public class ReservedHardware
  {
    public Guid HardwareId { get; set; }
    public Guid EventId { get; set; }
    public int Quantity { get; set; }
    public DateTime ReserveDate { get; set; }
    public string Name { get; set; }
  }
}
