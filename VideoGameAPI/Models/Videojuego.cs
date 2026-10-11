namespace VideoGameAPI.Models;

public class Videojuego
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public decimal Precio { get; set; }
    public string Categoria { get; set; } = string.Empty;
    public string ImagenUrl { get; set; } = string.Empty;

    public int DesarrolladorId { get; set; }
    public Desarrollador? Desarrollador { get; set; }

    public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
}
