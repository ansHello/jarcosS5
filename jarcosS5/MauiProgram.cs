using jarcosS5.Repositories;
using jarcosS5.Utils;
using Microsoft.Extensions.Logging;

namespace jarcosS5
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            string dbPath = FileAccessHelper.GetFolderPath("personaDB.db3");
            builder.Services.AddSingleton<PersonaRepository>
                (s=> ActivatorUtilities.CreateInstance<PersonaRepository>(s, dbPath));
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
