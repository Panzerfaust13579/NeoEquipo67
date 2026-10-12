using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers;

public class ResenasController(AppDbContext context) : ODataController
{

    [EnableQuery]
    public ActionResult<IQueryable<Resena>> Get()
    {
        return Ok(context.Resenas);
    }

    [HttpGet("odata/Reviews({key})")]
    public ActionResult Get([FromRoute] int key)
    {
        var resena = context.Resenas.SingleOrDefault(r => r.Id == key);
        if (resena == null)
        {
            return NotFound();
        }
        return Ok(resena);
    }

    public ActionResult Post([FromBody] Resena resena)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        context.Resenas.Add(resena);
        context.SaveChanges();

        return Created(resena);
    }

    public ActionResult Put([FromRoute] int key, [FromBody] Resena update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (key != update.Id)
        {
            return BadRequest("El id no coincide con la clave proporcionada.");
        }

        var existing = context.Resenas.SingleOrDefault(r => r.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Calificacion = update.Calificacion;
        existing.Comentario = update.Comentario;
        existing.Autor = update.Autor;
        existing.VideojuegoId = update.VideojuegoId;

        context.SaveChanges();
        return Updated(existing);
    }

    public ActionResult Patch([FromRoute] int key, [FromBody] Delta<Resena> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = context.Resenas.SingleOrDefault(r => r.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        delta.Patch(existing);
        context.SaveChanges();

        return Updated(existing);
    }

    public ActionResult Delete([FromRoute] int key)
    {
        var existing = context.Resenas.SingleOrDefault(r => r.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        context.Resenas.Remove(existing);
        context.SaveChanges();

        return NoContent();
    }
}
