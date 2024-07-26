using Api.Utilities;
using Api.Domain;

namespace Api
{
	internal class ServiceRegistrator
	{
		internal static void RegisterServices(IServiceCollection services)
		{
            _ = services
                .AddSingleton<IApiClient, ApiClient>()
                .AddSingleton<IItemsService, ItemsService>()
                .AddSingleton(services =>
                {
                    var configuration = services.GetRequiredService<IConfiguration>();
                    var section = configuration.GetRequiredSection(nameof(ApiSettings));
                    var settings = section.Get<ApiSettings>()!;
                    return settings;
                });
        }
	}
}

