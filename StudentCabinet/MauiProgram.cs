using Microsoft.Extensions.Logging;
using StudentCabinet.Services;
using StudentCabinet.ViewModels;

namespace StudentCabinet;

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

#if DEBUG
        builder.Logging.AddDebug();
#endif

        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "students.db3");

        builder.Services.AddSingleton<IAppStateService, AppStateService>();
        builder.Services.AddSingleton<IStudentRepository>(sp => new StudentRepository(dbPath));
        builder.Services.AddSingleton<StudentViewModel>();
        builder.Services.AddTransient<MainPage>();

        return builder.Build();
    }
}