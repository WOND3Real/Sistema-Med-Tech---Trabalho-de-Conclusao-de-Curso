using Microsoft.Extensions.Logging;

namespace SmtMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		
		builder.Services.AddSingleton<HttpClient>();  // Registra o HttpClient para injeção
		builder.Services.AddSingleton<PainelADM>(); 

		builder.UseMauiApp<App>().ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
