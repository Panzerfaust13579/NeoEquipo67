using System.Net.Http.Json;
using System.Text.Json;
using VideoGameList.Models;

namespace VideoGameList.Services;

public class VideojuegoService(HttpClient httpClient)
{
    public async Task<List<VideojuegoDto>> GetVideojuegosAsync(bool includeDetails)
    {
        try
        {
            var url = includeDetails ? "Videojuegos?$expand=Desarrollador,Resenas" : "Videojuegos"; 
            var response = await httpClient.GetFromJsonAsync<ODataResponse<VideojuegoDto>>(url);
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
            var response = await httpClient.GetFromJsonAsync<ODataResponse<VideojuegoDto>>($"Videojuegos?$filter=Id eq {id}&$expand=Desarrollador,Resenas");
            var juego = response?.Value?.FirstOrDefault();
            if (juego != null)
            {
                return juego;
            }

            return await httpClient.GetFromJsonAsync<VideojuegoDto>($"Videojuegos({id})?$expand=Desarrollador,Resenas");
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
            var payload = new
            {
                nuevoVideojuego.Nombre,
                nuevoVideojuego.Descripcion,
                nuevoVideojuego.Precio,
                nuevoVideojuego.Categoria,
                nuevoVideojuego.ImagenUrl,
                nuevoVideojuego.DesarrolladorId
            };
            var response = await httpClient.PostAsJsonAsync("Videojuegos", payload);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<VideojuegoDto>();
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

            var response = await httpClient.GetFromJsonAsync<ODataResponse<VideojuegoDto>>(url);
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
            var response = await httpClient.PostAsJsonAsync("Resenas", payload);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ResenaDto>();
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
            var response = await httpClient.GetFromJsonAsync<ODataResponse<DesarrolladorDto>>("Desarrolladores?$expand=Videojuegos");
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
            var response = await httpClient.GetFromJsonAsync<ODataResponse<DesarrolladorDto>>($"Desarrolladores?$filter=Id eq {id}&$expand=Videojuegos");
            var desarrollador = response?.Value?.FirstOrDefault();
            if (desarrollador != null)
            {
                return desarrollador;
            }

            var url = $"Desarrolladores({id})?$expand=Videojuegos";
            return await httpClient.GetFromJsonAsync<DesarrolladorDto>(url);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener desarrollador {id}: {ex.Message}");
            throw;
        }
    }
}
