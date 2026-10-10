using Microsoft.Extensions.Logging;
using VideoGameList.Services;

namespace VideoGameList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // Configuración de URL base dinámica según la plataforma
            string baseUrl = DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5226/odata/"
                : "http://localhost:5226/odata/";

            builder.Services.AddHttpClient<IVideojuegoService, VideojuegoService>(client =>
            {
                client.BaseAddress = new Uri(baseUrl);
            });

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
