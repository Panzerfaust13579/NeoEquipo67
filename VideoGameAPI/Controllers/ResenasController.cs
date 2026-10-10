using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers;

public class ResenasController : ODataController
{
    private readonly AppDbContext _context;

    public ResenasController(AppDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Resenas);
    }

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var resena = _context.Resenas.Where(r => r.Id == key);
        if (!resena.Any())
        {
            return NotFound();
        }
        return Ok(SingleResult.Create(resena));
    }

    public async Task<IActionResult> Post([FromBody] Resena resena)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Resenas.Add(resena);
        await _context.SaveChangesAsync();

        return Created(resena);
    }

    public async Task<IActionResult> Put([FromRoute] int key, [FromBody] Resena update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (key != update.Id)
        {
            return BadRequest("El id no coincide con la clave proporcionada.");
        }

        var existing = await _context.Resenas.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Calificacion = update.Calificacion;
        existing.Comentario = update.Comentario;
        existing.Autor = update.Autor;
        existing.VideojuegoId = update.VideojuegoId;

        await _context.SaveChangesAsync();
        return Updated(existing);
    }

    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Resena> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.Resenas.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        delta.Patch(existing);
        await _context.SaveChangesAsync();

        return Updated(existing);
    }

    public async Task<IActionResult> Delete([FromRoute] int key)
    {
        var existing = await _context.Resenas.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        _context.Resenas.Remove(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
