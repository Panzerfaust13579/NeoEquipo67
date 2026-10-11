namespace VideoGameList.Models;

public class DesarrolladorDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Pais { get; set; } = string.Empty;
    public int AnioFundacion { get; set; }
    public string? SitioWeb { get; set; }

    public List<VideojuegoDto> Videojuegos { get; set; } = new();

    // Propiedades calculadas (null-safe)
    public int TotalVideojuegos => Videojuegos?.Count ?? 0;

    public decimal PrecioPromedio =>
        Videojuegos != null && Videojuegos.Count > 0
            ? Math.Round(Videojuegos.Average(v => v.Precio), 2)
            : 0;

    public double CalificacionPromedioDesarrollador =>
        Videojuegos != null && Videojuegos.Any(v => v.TotalResenas > 0)
            ? Math.Round(Videojuegos.Where(v => v.TotalResenas > 0).Average(v => v.CalificacionPromedio), 1)
            : 0;
}
