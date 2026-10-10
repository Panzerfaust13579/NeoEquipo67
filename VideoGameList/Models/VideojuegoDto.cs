using System.ComponentModel.DataAnnotations;

namespace VideoGameList.Models;

public class VideojuegoDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Máximo 500 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(0.01, 9999, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; } = 0.01m;

    [Required(ErrorMessage = "La categoría es obligatoria")]
    public string Categoria { get; set; } = string.Empty;

    [Url(ErrorMessage = "Debe ser una URL válida")]
    public string ImagenUrl { get; set; } = string.Empty;

    public List<ResenaDto> Resenas { get; set; } = new();

    // Propiedades calculadas (null-safe)
    public double CalificacionPromedio =>
        Resenas != null && Resenas.Count > 0
            ? Math.Round(Resenas.Average(r => r.Calificacion), 1)
            : 0;

    public int TotalResenas => Resenas?.Count ?? 0;
}
