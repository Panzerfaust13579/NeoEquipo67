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
            var response = await _httpClient.GetFromJsonAsync<ODataResponse<VideojuegoDto>>("Videojuegos?$expand=Resenas", JsonOptions);
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
            var response = await _httpClient.GetFromJsonAsync<VideojuegoDto>($"Videojuegos({id})?$expand=Resenas", JsonOptions);
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
            var response = await _httpClient.PostAsJsonAsync("Videojuegos", nuevoVideojuego);
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
                ? "Videojuegos?$expand=Resenas"
                : $"Videojuegos?$filter=Categoria eq '{Uri.EscapeDataString(categoria)}'&$expand=Resenas";

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
            var response = await _httpClient.PostAsJsonAsync("Resenas", nuevaResena);
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
}
