namespace Breweryinator.Shared.DTOs;

public class BeerDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal AlcoholByVolume { get; set; }
    public decimal InternationalBitternessUnits { get; set; }
    public string? Color { get; set; }
    public int BatchCount { get; set; }
}

public class CreateBeerDto
{
    public string Name { get; set; } = string.Empty;
    public string Style { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal AlcoholByVolume { get; set; }
    public decimal InternationalBitternessUnits { get; set; }
    public string? Color { get; set; }
}
