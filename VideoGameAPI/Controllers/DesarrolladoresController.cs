using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

namespace VideoGameAPI.Controllers;

public class DesarrolladoresController(AppDbContext context) : ODataController
{

    [EnableQuery]
    public ActionResult<IQueryable<Desarrollador>> Get()
    {
        return Ok(context.Desarrolladores);
    }

    [HttpGet("odata/Developers({key})")]
    public ActionResult Get([FromRoute] int key)
    {
        var desarrollador = context.Desarrolladores.SingleOrDefault(d => d.Id == key);
        if (desarrollador == null)
        {
            return NotFound();
        }
        return Ok(desarrollador);
    }

    public ActionResult Post([FromBody] Desarrollador desarrollador)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        context.Desarrolladores.Add(desarrollador);
        context.SaveChanges();

        return Created(desarrollador);
    }

    public ActionResult Put([FromRoute] int key, [FromBody] Desarrollador update)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (key != update.Id)
        {
            return BadRequest("El id no coincide con la clave proporcionada.");
        }

        var existing = context.Desarrolladores.FirstOrDefault(d => d.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        existing.Nombre = update.Nombre;
        existing.Pais = update.Pais;
        existing.AnioFundacion = update.AnioFundacion;
        existing.SitioWeb = update.SitioWeb;

        context.SaveChanges();
        return Updated(existing);
    }

    public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Desarrollador> delta)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existing = context.Desarrolladores.FirstOrDefault(d => d.Id == key);
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
        var existing = context.Desarrolladores.FirstOrDefault(d => d.Id == key);
        if (existing == null)
        {
            return NotFound();
        }

        context.Desarrolladores.Remove(existing);
        context.SaveChanges();

        return NoContent();
    }
}
