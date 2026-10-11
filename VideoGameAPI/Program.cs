using System.Text.Json.Serialization;
using Microsoft.AspNetCore.OData;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using VideoGameAPI.Data;
using VideoGameAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de SQLite en memoria persistente durante la vida de la app
var sqliteConnection = new SqliteConnection("DataSource=:memory:");
sqliteConnection.Open();
builder.Services.AddSingleton(sqliteConnection);

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connection = serviceProvider.GetRequiredService<SqliteConnection>();
    options.UseSqlite(connection);
});

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Modelo EDM para OData
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Desarrollador>("Desarrolladores");
modelBuilder.EntitySet<Videojuego>("Videojuegos");
modelBuilder.EntitySet<Resena>("Resenas");
var edmModel = modelBuilder.GetEdmModel();

// Configuración de Controllers y OData
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    })
    .AddOData(options =>
    {
        options.Select()
               .Filter()
               .Expand()
               .OrderBy()
               .Count()
               .SetMaxTop(100)
               .AddRouteComponents("odata", edmModel);
    });

builder.Services.AddOpenApi();

var app = builder.Build();

// Inicialización de la base de datos y semilla de datos
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(context);
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();
app.UseRouting();
app.MapControllers();

app.Run();
