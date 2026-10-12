using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers;

public class VideojuegosController(AppDbContext context) : ODataController
{

    [EnableQuery]
    public ActionResult<IQueryable<Videojuego>> Get()
    {
        return Ok(context.Videojuegos);
    }

    [HttpGet("odata/VideoGames({key})")]
    public ActionResult<Videojuego> Get([FromRoute] int key)
    {
        var juego = context.Videojuegos.SingleOrDefault(v => v.Id == key);
        if (juego == null)
        {
            return NotFound();
        }
        return Ok(juego);
    }

    public ActionResult Post([FromBody] Videojuego videojuego)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var desarrolladorExiste = context.Desarrolladores.SingleOrDefault(d => d.Id == videojuego.DesarrolladorId);
        if (desarrolladorExiste == null)
        {
            return BadRequest($"El desarrollador con ID {videojuego.DesarrolladorId} no existe.");
        }

        context.Videojuegos.Add(videojuego);
        context.SaveChanges();

        return Created(videojuego);
    }

    public ActionResult Put([FromRoute] int key, [FromBody] Videojuego update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (key != update.Id)
        {
            return BadRequest("El id no coincide con la clave proporcionada.");
        }

        var existing =  context.Videojuegos.SingleOrDefault(v => v.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        var desarrolladorExiste = context.Desarrolladores.SingleOrDefault(d => d.Id == update.DesarrolladorId);
        if (desarrolladorExiste == null)
        {
            return BadRequest($"El desarrollador con ID {update.DesarrolladorId} no existe.");
        }

        existing.Nombre = update.Nombre;
        existing.Descripcion = update.Descripcion;
        existing.Precio = update.Precio;
        existing.Categoria = update.Categoria;
        existing.ImagenUrl = update.ImagenUrl;
        existing.DesarrolladorId = update.DesarrolladorId;

        context.SaveChanges();
        return Updated(existing);
    }

    public ActionResult Patch([FromRoute] int key, [FromBody] Delta<Videojuego> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = context.Videojuegos.SingleOrDefault(v => v.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        delta.Patch(existing);

        var desarrolladorExiste = context.Desarrolladores.SingleOrDefault(d => d.Id == existing.DesarrolladorId);
        if (desarrolladorExiste == null)
        {
            return BadRequest($"El desarrollador con ID {existing.DesarrolladorId} no existe.");
        }

        context.SaveChanges();

        return Updated(existing);
    }

    public ActionResult Delete([FromRoute] int key)
    {
        var existing = context.Videojuegos.SingleOrDefault(v => v.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        context.Videojuegos.Remove(existing);
        context.SaveChanges();

        return NoContent();
    }

}   

  
