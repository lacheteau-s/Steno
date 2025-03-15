using CommunityToolkit.Maui;
using Fonts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Mobile.Services;
using Mobile.ViewModels;
using Mobile.Views;
using Refit;
using System.Reflection;

namespace Mobile;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.AddAppSettings()
			.ConfigureServices()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("FluentSystemIcons-Regular.ttf", FluentUI.FontFamily);
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}

	private static MauiAppBuilder ConfigureServices(this MauiAppBuilder builder)
	{
		var services = builder.Services;

		services.AddSingleton<MainViewModel>();
		services.AddTransientWithShellRoute<CreateNoteView, CreateNoteViewModel>("CreateNote");

		services.AddRefitClient<IApiClient>()
			.ConfigureHttpClient(c =>
			{
				var baseUrl = builder.Configuration.GetValue<string>("Api:BaseUrl");
				c.BaseAddress = new Uri(baseUrl!);
			});

		return builder;
	}

	private static MauiAppBuilder AddAppSettings(this MauiAppBuilder builder)
	{
#if DEBUG
		var env = "Development";
#endif
		using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"Mobile.appsettings.{env}.json");

		if (stream != null)
		{
			var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
			builder.Configuration.AddConfiguration(config);
		}

		return builder;
	}
}
