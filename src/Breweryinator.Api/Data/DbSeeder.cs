using Breweryinator.Shared.Models;

namespace Breweryinator.Api.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (context.Beers.Any())
            return;

        var beers = new List<Beer>
        {
            new()
            {
                Name = "Hoppy Lager",
                Style = "Czech Pilsner",
                Description = "A crisp, refreshing lager with a floral hop character.",
                AlcoholByVolume = 4.8m,
                InternationalBitternessUnits = 28,
                Color = "#F5C842",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Name = "Amber Ale",
                Style = "American Amber Ale",
                Description = "A malt-forward amber ale with caramel notes and a clean finish.",
                AlcoholByVolume = 5.4m,
                InternationalBitternessUnits = 35,
                Color = "#C45C14",
                CreatedAt = new DateTime(2024, 2, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                Name = "Dark Stout",
                Style = "Irish Dry Stout",
                Description = "A roasted, dark stout with coffee and chocolate undertones.",
                AlcoholByVolume = 4.2m,
                InternationalBitternessUnits = 42,
                Color = "#1A0A00",
                CreatedAt = new DateTime(2024, 3, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        context.Beers.AddRange(beers);
        context.SaveChanges();

        var batches = new List<Batch>
        {
            new()
            {
                BeerId = beers[0].Id,
                BatchNumber = "LAGER-2024-001",
                BrewDate = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc),
                PackagingDate = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc),
                BestBeforeDate = new DateTime(2025, 4, 15, 0, 0, 0, DateTimeKind.Utc),
                VolumeInLitres = 20m,
                Status = BatchStatus.Packaged,
                Notes = "First batch. Fermented at 10°C for 4 weeks.",
                CreatedAt = new DateTime(2024, 3, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                BeerId = beers[1].Id,
                BatchNumber = "AMBER-2024-001",
                BrewDate = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc),
                VolumeInLitres = 25m,
                Status = BatchStatus.Conditioning,
                Notes = "Experimenting with a higher mash temperature.",
                CreatedAt = new DateTime(2024, 4, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new()
            {
                BeerId = beers[2].Id,
                BatchNumber = "STOUT-2024-001",
                BrewDate = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc),
                VolumeInLitres = 20m,
                Status = BatchStatus.Fermenting,
                Notes = "Classic Irish stout recipe.",
                CreatedAt = new DateTime(2024, 5, 1, 0, 0, 0, DateTimeKind.Utc)
            }
        };

        context.Batches.AddRange(batches);
        context.SaveChanges();
    }
}
