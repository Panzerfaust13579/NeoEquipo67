using System.Collections.Generic;

namespace VideoGameAPI.Models;

public class Desarrollador
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public int AnioFundacion { get; set; }
    public string? SitioWeb { get; set; }
    public ICollection<Videojuego> Videojuegos { get; set; } = [];
}

