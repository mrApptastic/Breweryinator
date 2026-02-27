using Breweryinator.Api.Data;
using Breweryinator.Shared.DTOs;
using Breweryinator.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Breweryinator.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class BeersController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BeerDto>>> GetAll()
    {
        var beers = await db.Beers
            .Include(b => b.Batches)
            .Select(b => new BeerDto
            {
                Id = b.Id,
                Name = b.Name,
                Style = b.Style,
                Description = b.Description,
                AlcoholByVolume = b.AlcoholByVolume,
                InternationalBitternessUnits = b.InternationalBitternessUnits,
                Color = b.Color,
                BatchCount = b.Batches.Count
            })
            .ToListAsync();

        return Ok(beers);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BeerDto>> GetById(int id)
    {
        var beer = await db.Beers
            .Include(b => b.Batches)
            .Where(b => b.Id == id)
            .Select(b => new BeerDto
            {
                Id = b.Id,
                Name = b.Name,
                Style = b.Style,
                Description = b.Description,
                AlcoholByVolume = b.AlcoholByVolume,
                InternationalBitternessUnits = b.InternationalBitternessUnits,
                Color = b.Color,
                BatchCount = b.Batches.Count
            })
            .FirstOrDefaultAsync();

        if (beer is null) return NotFound();
        return Ok(beer);
    }

    [HttpPost]
    public async Task<ActionResult<BeerDto>> Create(CreateBeerDto dto)
    {
        var beer = new Beer
        {
            Name = dto.Name,
            Style = dto.Style,
            Description = dto.Description,
            AlcoholByVolume = dto.AlcoholByVolume,
            InternationalBitternessUnits = dto.InternationalBitternessUnits,
            Color = dto.Color
        };

        db.Beers.Add(beer);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = beer.Id }, new BeerDto
        {
            Id = beer.Id,
            Name = beer.Name,
            Style = beer.Style,
            Description = beer.Description,
            AlcoholByVolume = beer.AlcoholByVolume,
            InternationalBitternessUnits = beer.InternationalBitternessUnits,
            Color = beer.Color
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreateBeerDto dto)
    {
        var beer = await db.Beers.FindAsync(id);
        if (beer is null) return NotFound();

        beer.Name = dto.Name;
        beer.Style = dto.Style;
        beer.Description = dto.Description;
        beer.AlcoholByVolume = dto.AlcoholByVolume;
        beer.InternationalBitternessUnits = dto.InternationalBitternessUnits;
        beer.Color = dto.Color;
        beer.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var beer = await db.Beers.FindAsync(id);
        if (beer is null) return NotFound();

        db.Beers.Remove(beer);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
