namespace Breweryinator.Shared.Models;

public class Beer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal AlcoholByVolume { get; set; }
    public decimal InternationalBitternessUnits { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public ICollection<Batch> Batches { get; set; } = new List<Batch>();
}
