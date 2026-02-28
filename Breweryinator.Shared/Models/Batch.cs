namespace Breweryinator.Shared.Models;

public class Batch
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public Beer Beer { get; set; } = null!;
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime BrewDate { get; set; }
    public DateTime? PackagingDate { get; set; }
    public DateTime? BestBeforeDate { get; set; }
    public decimal VolumeInLitres { get; set; }
    public BatchStatus Status { get; set; } = BatchStatus.Planned;
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum BatchStatus
{
    Planned,
    Brewing,
    Fermenting,
    Conditioning,
    Packaged,
    Consumed
}
