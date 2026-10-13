namespace VideoGameList.Models;

public class ResenaDto
{
    public int Id { get; set; }
    public int VideojuegoId { get; set; }
    public int Calificacion { get; set; }
    public string Comentario { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
}
