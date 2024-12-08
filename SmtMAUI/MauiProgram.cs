using Microsoft.Extensions.Logging;

namespace SmtMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Services.AddSingleton<HttpClient>(); // Adiciona HttpClient
		builder.Services.AddSingleton<Login>(); // Adiciona LoginService
        builder.Services.AddSingleton<ListaConsulta>();
        builder.Services.AddSingleton<CadastroContribuintes>();
        builder.Services.AddSingleton<PainelMedico>();
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

		return builder.Build();
	}
}
