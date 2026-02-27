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
public class BatchesController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BatchDto>>> GetAll([FromQuery] int? beerId = null)
    {
        var query = db.Batches.Include(b => b.Beer).AsQueryable();

        if (beerId.HasValue)
            query = query.Where(b => b.BeerId == beerId.Value);

        var batches = await query
            .Select(b => new BatchDto
            {
                Id = b.Id,
                BeerId = b.BeerId,
                BeerName = b.Beer.Name,
                BatchNumber = b.BatchNumber,
                BrewDate = b.BrewDate,
                PackagingDate = b.PackagingDate,
                BestBeforeDate = b.BestBeforeDate,
                VolumeInLitres = b.VolumeInLitres,
                Status = b.Status,
                Notes = b.Notes
            })
            .ToListAsync();

        return Ok(batches);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<BatchDto>> GetById(int id)
    {
        var batch = await db.Batches
            .Include(b => b.Beer)
            .Where(b => b.Id == id)
            .Select(b => new BatchDto
            {
                Id = b.Id,
                BeerId = b.BeerId,
                BeerName = b.Beer.Name,
                BatchNumber = b.BatchNumber,
                BrewDate = b.BrewDate,
                PackagingDate = b.PackagingDate,
                BestBeforeDate = b.BestBeforeDate,
                VolumeInLitres = b.VolumeInLitres,
                Status = b.Status,
                Notes = b.Notes
            })
            .FirstOrDefaultAsync();

        if (batch is null) return NotFound();
        return Ok(batch);
    }

    [HttpPost]
    public async Task<ActionResult<BatchDto>> Create(CreateBatchDto dto)
    {
        if (!await db.Beers.AnyAsync(b => b.Id == dto.BeerId))
            return BadRequest("Beer not found.");

        var batch = new Batch
        {
            BeerId = dto.BeerId,
            BatchNumber = dto.BatchNumber,
            BrewDate = dto.BrewDate,
            PackagingDate = dto.PackagingDate,
            BestBeforeDate = dto.BestBeforeDate,
            VolumeInLitres = dto.VolumeInLitres,
            Status = dto.Status,
            Notes = dto.Notes
        };

        db.Batches.Add(batch);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = batch.Id }, new BatchDto
        {
            Id = batch.Id,
            BeerId = batch.BeerId,
            BeerName = (await db.Beers.FindAsync(batch.BeerId))!.Name,
            BatchNumber = batch.BatchNumber,
            BrewDate = batch.BrewDate,
            PackagingDate = batch.PackagingDate,
            BestBeforeDate = batch.BestBeforeDate,
            VolumeInLitres = batch.VolumeInLitres,
            Status = batch.Status,
            Notes = batch.Notes
        });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CreateBatchDto dto)
    {
        var batch = await db.Batches.FindAsync(id);
        if (batch is null) return NotFound();

        batch.BeerId = dto.BeerId;
        batch.BatchNumber = dto.BatchNumber;
        batch.BrewDate = dto.BrewDate;
        batch.PackagingDate = dto.PackagingDate;
        batch.BestBeforeDate = dto.BestBeforeDate;
        batch.VolumeInLitres = dto.VolumeInLitres;
        batch.Status = dto.Status;
        batch.Notes = dto.Notes;
        batch.UpdatedAt = DateTime.UtcNow;

        await db.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var batch = await db.Batches.FindAsync(id);
        if (batch is null) return NotFound();

        db.Batches.Remove(batch);
        await db.SaveChangesAsync();
        return NoContent();
    }
}
