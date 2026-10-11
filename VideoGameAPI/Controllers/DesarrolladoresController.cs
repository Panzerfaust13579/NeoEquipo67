using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers;

public class DesarrolladoresController : ODataController
{
    private readonly AppDbContext _context;

    public DesarrolladoresController(AppDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Desarrolladores);
    }

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var desarrollador = _context.Desarrolladores.Where(d => d.Id == key);
        if (!desarrollador.Any())
        {
            return NotFound();
        }
        return Ok(SingleResult.Create(desarrollador));
    }

    public async Task<IActionResult> Post([FromBody] Desarrollador desarrollador)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Desarrolladores.Add(desarrollador);
        await _context.SaveChangesAsync();

        return Created(desarrollador);
    }

    public async Task<IActionResult> Put([FromRoute] int key, [FromBody] Desarrollador update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (key != update.Id)
        {
            return BadRequest("El id no coincide con la clave proporcionada.");
        }

        var existing = await _context.Desarrolladores.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Nombre = update.Nombre;
        existing.Pais = update.Pais;
        existing.AnioFundacion = update.AnioFundacion;
        existing.SitioWeb = update.SitioWeb;

        await _context.SaveChangesAsync();
        return Updated(existing);
    }

    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Desarrollador> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.Desarrolladores.FindAsync(key);
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
        var existing = await _context.Desarrolladores.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        _context.Desarrolladores.Remove(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
