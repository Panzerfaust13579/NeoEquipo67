using System.Net.Http.Json;
using System.Text.Json;
using VideoGameList.Models;

namespace VideoGameList.Services;

public class VideojuegoService : IVideojuegoService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public VideojuegoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<VideojuegoDto>> GetVideojuegosAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<VideojuegoDto>>("Videojuegos?$expand=Desarrollador,Resenas", JsonOptions);
            return response?.Value ?? new List<VideojuegoDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener videojuegos: {ex.Message}");
            throw;
        }
    }

    public async Task<VideojuegoDto?> GetVideojuegoAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<VideojuegoDto>($"Videojuegos({id})?$expand=Desarrollador,Resenas", JsonOptions);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener videojuego {id}: {ex.Message}");
            throw;
        }
    }

    public async Task<VideojuegoDto?> CrearVideojuegoAsync(VideojuegoDto nuevoVideojuego)
    {
        try
        {
            // OData solo acepta las propiedades escalares de la entidad (PascalCase, sin calculadas ni navegaciones)
            var payload = new
            {
                nuevoVideojuego.Nombre,
                nuevoVideojuego.Descripcion,
                nuevoVideojuego.Precio,
                nuevoVideojuego.Categoria,
                nuevoVideojuego.ImagenUrl,
                nuevoVideojuego.DesarrolladorId
            };
            var response = await _httpClient.PostAsJsonAsync("Videojuegos", payload, JsonOptions);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VideojuegoDto>(JsonOptions);
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al crear videojuego: {response.StatusCode} - {errorBody}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear videojuego: {ex.Message}");
            throw;
        }
    }

    public async Task<List<VideojuegoDto>> BuscarPorCategoriaAsync(string categoria)
    {
        try
        {
            string url = string.IsNullOrWhiteSpace(categoria)
                ? "Videojuegos?$expand=Desarrollador,Resenas"
                : $"Videojuegos?$filter=Categoria eq '{Uri.EscapeDataString(categoria)}'&$expand=Desarrollador,Resenas";

            var response = await _httpClient.GetFromJsonAsync<ODataResponse<VideojuegoDto>>(url, JsonOptions);
            return response?.Value ?? new List<VideojuegoDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al buscar videojuegos por categoría: {ex.Message}");
            throw;
        }
    }

    public async Task<ResenaDto?> CrearResenaAsync(ResenaDto nuevaResena)
    {
        try
        {
            var payload = new
            {
                nuevaResena.VideojuegoId,
                nuevaResena.Calificacion,
                nuevaResena.Comentario,
                nuevaResena.Autor
            };
            var response = await _httpClient.PostAsJsonAsync("Resenas", payload, JsonOptions);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ResenaDto>(JsonOptions);
            }

            var errorBody = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Error al crear reseña: {response.StatusCode} - {errorBody}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al crear reseña: {ex.Message}");
            throw;
        }
    }

    public async Task<List<DesarrolladorDto>> GetDesarrolladoresAsync()
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<DesarrolladorDto>>("Desarrolladores?$expand=Videojuegos", JsonOptions);
            return response?.Value ?? new List<DesarrolladorDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener desarrolladores: {ex.Message}");
            throw;
        }
    }

    public async Task<DesarrolladorDto?> GetDesarrolladorAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetFromJsonAsync<DesarrolladorDto>($"Desarrolladores({id})?$expand=Videojuegos", JsonOptions);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener desarrollador {id}: {ex.Message}");
            throw;
        }
    }
}
