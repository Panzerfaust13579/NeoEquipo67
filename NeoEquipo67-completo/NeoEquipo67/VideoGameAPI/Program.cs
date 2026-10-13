using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

var builder = WebApplication.CreateSlimBuilder(args);

// Configuración de SQLite en memoria persistente durante la vida de la app
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Desarrollador>("Desarrolladores");
modelBuilder.EntitySet<Videojuego>("Videojuegos");
modelBuilder.EntitySet<Resena>("Resenas");

builder.Services.AddControllers().AddOData(options =>
{
    options.AddRouteComponents("odata", modelBuilder.GetEdmModel());
    options.EnableQueryFeatures();
});


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("VideoGameDb");
});

var app = builder.Build();
app.MapControllers();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope()) 
{
    var db = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

    SeedData.Initialize(db); // Llama al método Initialize de SeedData
}

 if (app.Environment.IsDevelopment())
 {
        
 }

app.Run();
