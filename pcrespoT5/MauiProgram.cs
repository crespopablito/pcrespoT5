using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using pcrespoT5.Repositories;
using pcrespoT5.Utils;

namespace pcrespoT5
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

            string dbPath = FileAccessHelper.GetLocalFilePath("uisrael.db");

            builder.Services.AddSingleton<PersonRepository>(s =>
                ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
