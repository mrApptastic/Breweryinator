using Breweryinator.Shared.Models;

namespace Breweryinator.Shared.DTOs;

public class BatchDto
{
    public int Id { get; set; }
    public int BeerId { get; set; }
    public string BeerName { get; set; } = string.Empty;
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime BrewDate { get; set; }
    public DateTime? PackagingDate { get; set; }
    public DateTime? BestBeforeDate { get; set; }
    public decimal VolumeInLitres { get; set; }
    public BatchStatus Status { get; set; }
    public string? Notes { get; set; }
}

public class CreateBatchDto
{
    public int BeerId { get; set; }
    public string BatchNumber { get; set; } = string.Empty;
    public DateTime BrewDate { get; set; }
    public DateTime? PackagingDate { get; set; }
    public DateTime? BestBeforeDate { get; set; }
    public decimal VolumeInLitres { get; set; }
    public BatchStatus Status { get; set; } = BatchStatus.Planned;
    public string? Notes { get; set; }
}
