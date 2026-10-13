namespace VideoGameList.Models;

using System.ComponentModel.DataAnnotations;

public class ResenaDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El ID del videojuego es requerido")]
    public int VideojuegoId { get; set; }
    
    [Required(ErrorMessage = "La calificación es requerida")]
    [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5")]
    public int Calificacion { get; set; }
    
    [StringLength(200, ErrorMessage = "El comentario no puede exceder los 200 caracteres")]
    public string Comentario { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "El autor es requerido")]
    [StringLength(100, ErrorMessage = "El nombre del autor no puede exceder los 100 caracteres")]
    public string Autor { get; set; } = string.Empty;
}
