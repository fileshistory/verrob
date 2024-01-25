namespace Shared.Models.Entities;

public record TransactionDetails
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public double Sum { set; get; }
    public DateTime ProcessingDate { get; set; }
    public DateTime Picture { get; set; }
    
    public Guid TypeId { get; set; }
    public Guid StatusId { get; set; } 
    public Guid CardId { get; set; }
}