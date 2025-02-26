using Microsoft.Extensions.DependencyInjection;
using Stopify.Presentation.ViewModels.Main;

namespace Stopify.Presentation;

public static class PresentationDependencies
{
    public static void AddPresentationDependencies(this IServiceCollection services)
    {
        services.AddSingleton<MainWindow>()
        .AddSingleton<MainViewModel>();
    }
}
