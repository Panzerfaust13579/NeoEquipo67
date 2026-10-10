using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers;

public class VideojuegosController : ODataController
{
    private readonly AppDbContext _context;

    public VideojuegosController(AppDbContext context)
    {
        _context = context;
    }

    [EnableQuery]
    public IActionResult Get()
    {
        return Ok(_context.Videojuegos);
    }

    [EnableQuery]
    public IActionResult Get([FromRoute] int key)
    {
        var juego = _context.Videojuegos.Where(v => v.Id == key);
        if (!juego.Any())
        {
            return NotFound();
        }
        return Ok(SingleResult.Create(juego));
    }

    public async Task<IActionResult> Post([FromBody] Videojuego videojuego)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Videojuegos.Add(videojuego);
        await _context.SaveChangesAsync();

        return Created(videojuego);
    }

    public async Task<IActionResult> Put([FromRoute] int key, [FromBody] Videojuego update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (key != update.Id)
        {
            return BadRequest("El id no coincide con la clave proporcionada.");
        }

        var existing = await _context.Videojuegos.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Nombre = update.Nombre;
        existing.Descripcion = update.Descripcion;
        existing.Precio = update.Precio;
        existing.Categoria = update.Categoria;
        existing.ImagenUrl = update.ImagenUrl;

        await _context.SaveChangesAsync();
        return Updated(existing);
    }

    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Videojuego> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = await _context.Videojuegos.FindAsync(key);
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
        var existing = await _context.Videojuegos.FindAsync(key);
        if (existing == null)
        {
            return NotFound();
        }

        _context.Videojuegos.Remove(existing);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
