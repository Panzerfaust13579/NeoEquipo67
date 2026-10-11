using VideoGameAPI.Models;

namespace VideoGameAPI.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (context.Desarrolladores.Any() || context.Videojuegos.Any())
        {
            return;
        }

        var devNintendo = new Desarrollador
        {
            Nombre = "Nintendo EPD",
            Pais = "Japón",
            AnioFundacion = 1983,
            SitioWeb = "https://www.nintendo.com"
        };

        var devFromSoftware = new Desarrollador
        {
            Nombre = "FromSoftware",
            Pais = "Japón",
            AnioFundacion = 1986,
            SitioWeb = "https://www.fromsoftware.jp"
        };

        var devTeamCherry = new Desarrollador
        {
            Nombre = "Team Cherry",
            Pais = "Australia",
            AnioFundacion = 2014,
            SitioWeb = "https://www.teamcherry.com.au"
        };

        var devCdProjekt = new Desarrollador
        {
            Nombre = "CD Projekt Red",
            Pais = "Polonia",
            AnioFundacion = 2002,
            SitioWeb = "https://www.cdprojektred.com"
        };

        var devPlayground = new Desarrollador
        {
            Nombre = "Playground Games",
            Pais = "Reino Unido",
            AnioFundacion = 2009,
            SitioWeb = "https://www.playground-games.com"
        };

        var devSupergiant = new Desarrollador
        {
            Nombre = "Supergiant Games",
            Pais = "Estados Unidos",
            AnioFundacion = 2009,
            SitioWeb = "https://www.supergiantgames.com"
        };

        var devLarian = new Desarrollador
        {
            Nombre = "Larian Studios",
            Pais = "Bélgica",
            AnioFundacion = 1996,
            SitioWeb = "https://larian.com"
        };

        var desarrolladores = new List<Desarrollador>
        {
            devNintendo,
            devFromSoftware,
            devTeamCherry,
            devCdProjekt,
            devPlayground,
            devSupergiant,
            devLarian
        };

        context.Desarrolladores.AddRange(desarrolladores);

        var juegos = new List<Videojuego>
        {
            new()
            {
                Nombre = "The Legend of Zelda: Tears of the Kingdom",
                Descripcion = "Una épica aventura a través de los cielos y tierras de Hyrule con creatividad y exploración sin límites.",
                Precio = 69.99m,
                Categoria = "Aventura",
                ImagenUrl = "https://images.unsplash.com/photo-1550745165-9bc0b252726f?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devNintendo,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Una obra maestra de la exploración y la física interactiva.", Autor = "Carlos Gómez" },
                    new() { Calificacion = 5, Comentario = "Supera al original en casi todos los aspectos técnicos y jugables.", Autor = "Lucía Fernández" },
                    new() { Calificacion = 4, Comentario = "Excelente juego, aunque la tasa de cuadros por segundo baja en ciertas zonas.", Autor = "Marcos Peña" }
                }
            },
            new()
            {
                Nombre = "Elden Ring",
                Descripcion = "Un desafiante juego de rol de acción en las Tierras Intermedias creado por Hidetaka Miyazaki y George R.R. Martin.",
                Precio = 59.99m,
                Categoria = "RPG",
                ImagenUrl = "https://images.unsplash.com/photo-1538481199705-c710c4e965fc?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devFromSoftware,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "El mejor diseño de mundo abierto en un Souls-like hasta la fecha.", Autor = "Diego Ramírez" },
                    new() { Calificacion = 5, Comentario = "Extenso, difícil y profundamente gratificante al superar cada jefe.", Autor = "Elena Morales" },
                    new() { Calificacion = 4, Comentario = "Curva de dificultad pronunciada pero increíble atmósfera.", Autor = "Roberto S." },
                    new() { Calificacion = 5, Comentario = "Diseño artístico insuperable.", Autor = "Ana Beltrán" }
                }
            },
            new()
            {
                Nombre = "Hollow Knight",
                Descripcion = "Forja tu propio camino en Hallownest, un vasto reino en ruinas lleno de insectos y héroes legendarios.",
                Precio = 14.99m,
                Categoria = "Metroidvania",
                ImagenUrl = "https://images.unsplash.com/photo-1511512578047-dfb367046420?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devTeamCherry,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Banda sonora y dirección artística insuperables por este precio.", Autor = "Sofía Herrera" },
                    new() { Calificacion = 4, Comentario = "Combate preciso y desafiante; exploración muy inmersiva.", Autor = "Alejandro Ruiz" }
                }
            },
            new()
            {
                Nombre = "Cyberpunk 2077",
                Descripcion = "Un RPG de acción de mundo abierto ambientado en la megalópolis de Night City, obsesionada con el poder y las modificaciones corporales.",
                Precio = 49.99m,
                Categoria = "RPG",
                ImagenUrl = "https://images.unsplash.com/photo-1542751371-adc38448a05e?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devCdProjekt,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 4, Comentario = "Tras las últimas actualizaciones se convirtió en una joya imperdible.", Autor = "Javier Ortiz" },
                    new() { Calificacion = 3, Comentario = "Buena historia, pero todavía tiene pequeños bugs visuales menores.", Autor = "Natalia Vega" },
                    new() { Calificacion = 4, Comentario = "Gráficos de siguiente generación y trama madura envolvente.", Autor = "Tomás Vidal" }
                }
            },
            new()
            {
                Nombre = "Super Mario Odyssey",
                Descripcion = "Embárcate en una aventura trotamundos en 3D junto a Mario y su nuevo amigo Cappy.",
                Precio = 49.99m,
                Categoria = "Plataformas",
                ImagenUrl = "https://images.unsplash.com/photo-1580234811497-9df7fd2f357e?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devNintendo,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Pura diversión e inventiva en cada reino que visitas.", Autor = "Mariana Ríos" },
                    new() { Calificacion = 5, Comentario = "Los controles responden con una precisión perfecta.", Autor = "Gonzalo Silva" },
                    new() { Calificacion = 4, Comentario = "Ideal para todas las edades con gran contenido post-juego.", Autor = "Patricia Luján" }
                }
            },
            new()
            {
                Nombre = "Forza Horizon 5",
                Descripcion = "Lidera impresionantes expediciones a través de los vibrantes y cambiantes paisajes de México en cientos de los mejores autos del mundo.",
                Precio = 59.99m,
                Categoria = "Carreras",
                ImagenUrl = "https://images.unsplash.com/photo-1607604276583-eef5d076aa5f?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devPlayground,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Gráficos fotorrealistas y la conducción más divertida del género.", Autor = "Andrés Molina" },
                    new() { Calificacion = 4, Comentario = "El mapa es gigantesco y repleto de actividades variadas.", Autor = "Valeria Castro" },
                    new() { Calificacion = 3, Comentario = "El modo multijugador a veces tarda en conectar salas.", Autor = "Ignacio Domínguez" }
                }
            },
            new()
            {
                Nombre = "Hades",
                Descripcion = "Desafía al dios de los muertos mientras hackeas y cortas para escapar del Inframundo en este roguelike de acción.",
                Precio = 24.99m,
                Categoria = "Roguelike",
                ImagenUrl = "https://images.unsplash.com/photo-1579373903781-fd5c0c30c4cd?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devSupergiant,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Narrativa brillante y jugabilidad adictiva; cada intento es único.", Autor = "Paula Méndez" },
                    new() { Calificacion = 5, Comentario = "Actuaciones de voz de primer nivel y música electrizante.", Autor = "Esteban Navarro" }
                }
            },
            new()
            {
                Nombre = "Baldur's Gate 3",
                Descripcion = "Reúne a tu grupo y regresa a los Reinos Olvidados en una historia de compañerismo y traición, sacrificio y supervivencia.",
                Precio = 59.99m,
                Categoria = "RPG",
                ImagenUrl = "https://images.unsplash.com/photo-1552824796-7c08974a638a?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devLarian,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Libertad de rol inigualable. Cada decisión realmente importa.", Autor = "Rodrigo Pardo" },
                    new() { Calificacion = 5, Comentario = "Uno de los mejores juegos de rol de la historia de los videojuegos.", Autor = "Claudia Santoro" },
                    new() { Calificacion = 4, Comentario = "El acto final tiene un ritmo algo apresurado, pero sigue siendo un 10.", Autor = "Felipe Bravo" },
                    new() { Calificacion = 5, Comentario = "Personajes inolvidables con arcos narrativos profundos.", Autor = "Daniela Solís" }
                }
            },
            new()
            {
                Nombre = "The Witcher 3: Wild Hunt",
                Descripcion = "Conviértete en un cazador de monstruos a sueldo y emprende un viaje épico para encontrar a la niña de la profecía.",
                Precio = 39.99m,
                Categoria = "RPG",
                ImagenUrl = "https://images.unsplash.com/photo-1518709268805-4e9042af9f23?w=600&auto=format&fit=crop&q=80",
                Desarrollador = devCdProjekt,
                Resenas = new List<Resena>
                {
                    new() { Calificacion = 5, Comentario = "Una de las mejores narrativas y misiones secundarias jamás escritas.", Autor = "Gabriel Soto" },
                    new() { Calificacion = 5, Comentario = "Geralt de Rivia en su máxima expresión. Mundo vivo y fascinante.", Autor = "Camila Rojas" }
                }
            }
        };

        context.Videojuegos.AddRange(juegos);
        context.SaveChanges();
    }
}
