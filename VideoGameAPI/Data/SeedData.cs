using VideoGameAPI.Models;

namespace VideoGameAPI.Data;

public static class SeedData
{
    public static void Initialize(AppDbContext context)
    {
        if (!context.Desarrolladores.Any() || !context.Videojuegos.Any())
        {
            var desarrolladores = new List<Desarrollador>
            {
                new ()
                {Nombre = "Nintendo EPD",
                 Pais = "Japón", AnioFundacion = 1983,
                 SitioWeb = "https://www.nintendo.com/es-mx/"
                },

                new ()
                {Nombre = "FromSoftware",
                 Pais = "Japón", AnioFundacion = 1986,
                 SitioWeb = "https://www.fromsoftware.jp/ww/"
                },
                new ()
                {Nombre = "Team Cherry",
                 Pais = "Australia", AnioFundacion = 2014,
                 SitioWeb = "https://www.teamcherry.com.au/"
                },
                new ()
                {Nombre = "CD Projekt Red",
                 Pais = "Polonia", AnioFundacion = 2002,
                 SitioWeb = "https://www.cdprojektred.com/en"
                },
                new ()
                {Nombre = "Playground Games",
                 Pais = "Reino Unido", AnioFundacion = 2009,
                 SitioWeb = "https://playground-games.com/"
                },
                new ()
                {Nombre = "Supergiant Games",
                 Pais = "Estados Unidos", AnioFundacion = 2009,
                 SitioWeb = "https://www.supergiantgames.com/"},
                new ()
                {Nombre = "Larian Studios",
                 Pais = "Bélgica", AnioFundacion = 1996,
                 SitioWeb = "https://larian.com/"}
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
                    ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS_dqydZU8vcftgBtTv3IwBZ8QUdaCALCcmcWngXAf0RQ&s=10",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "Nintendo EPD"),
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
                    ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSAmb9QhLh1l_1Uro8o1a_VNDp90zg5x6eMmwETCeDp4b_F76Za3oxxv30&s=10",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "FromSoftware"),
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
                    ImagenUrl = "https://upload.wikimedia.org/wikipedia/en/d/de/Hollow_Knight_2026_cover_art.jpg?utm_source=en.wikipedia.org&utm_campaign=index&utm_content=original",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "Team Cherry"),
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
                    ImagenUrl = "https://image.api.playstation.com/vulcan/ap/rnd/202008/0416/6Bo40lnWU0BhgrOUm7Cb6by3.png?w=440",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "CD Projekt Red"),
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
                    ImagenUrl = "https://assets.nintendo.com/image/upload/c_fill,w_1200/q_auto:best/f_auto/dpr_2.0/store/software/switch/70010000001130/c42553b4fd0312c31e70ec7468c6c9bccd739f340152925b9600631f2d29f8b5",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "Nintendo EPD"),
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
                    ImagenUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQL4hKrZ65FtYq3g4eUmzbfxPdkf-zfIhyNBw_X2ojLqm2-cOQo4OKfo3w&s=10",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "Playground Games"),
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
                    ImagenUrl = "https://assets.nintendo.com/image/upload/ar_16:9,c_lpad,w_1240/b_white/f_auto/q_auto/store/software/switch/70010000033131/dbc8c55a21688b446a5c57711b726956483a14ef8c5ddb861f897c0595ccb6b5",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "Supergiant Games"),
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
                    ImagenUrl = "https://store-images.s-microsoft.com/image/apps.11593.13550459053619040.9c555c73-a698-4992-b0f3-c5084cf18b5e.82a9ea41-c628-4d02-8a0f-d0304eba31c7",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "Larian Studios"),
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
                    ImagenUrl = "https://thumb.wikimedia.org/wikipedia/commons/thumb/0/0b/The_Witcher_3_-_Standard_Edition_Unboxing_%28Official_Trailer%29_cover.jpg/250px-The_Witcher_3_-_Standard_Edition_Unboxing_%28Official_Trailer%29_cover.jpg?utm_source=es.wikipedia.org&utm_campaign=parser&utm_content=thumbnail",
                    Desarrollador = desarrolladores.Find(d => d.Nombre == "CD Projekt Red"),
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
}
