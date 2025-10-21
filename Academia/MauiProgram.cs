using Academia.MVVM.ViewModels;
using Academia.MVVM.Views;
using Academia.Services;

namespace Academia;
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

        // Serviços
        builder.Services.AddSingleton<IAcademiaDbService, AcademiaDbService>();

        // ViewModels
        builder.Services.AddSingleton<MainViewModel>();
        builder.Services.AddTransient<AddExercicioViewModel>();
        builder.Services.AddTransient<ExercicioDetailViewModel>();

        // Views
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<AddExercicioPage>();
        builder.Services.AddTransient<ExercicioDetailPage>();

        return builder.Build();
	}
}
