using VideoGameList.Models;

namespace VideoGameList.Services;

public interface IVideojuegoService
{
    Task<List<VideojuegoDto>> GetVideojuegosAsync();
    Task<VideojuegoDto?> GetVideojuegoAsync(int id);
    Task<VideojuegoDto?> CrearVideojuegoAsync(VideojuegoDto nuevoVideojuego);
    Task<List<VideojuegoDto>> BuscarPorCategoriaAsync(string categoria);
    Task<ResenaDto?> CrearResenaAsync(ResenaDto nuevaResena);

    Task<List<DesarrolladorDto>> GetDesarrolladoresAsync();
    Task<DesarrolladorDto?> GetDesarrolladorAsync(int id);
}
