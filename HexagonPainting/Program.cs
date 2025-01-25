using Avalonia;
using Avalonia.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using HexagonPainting.Core.Common.Interfaces;
using HexagonPainting.Core.Common.Models;
using HexagonPainting.Core.Map.Interfaces;
using HexagonPainting.Logic.Map.Maps;
using HexagonPainting_ViewModels;
using HexagonPainting_ViewModels.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HexagonPainting
{
    internal sealed class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args)
        {
            ConfigureServices();

            BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        }

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
            => AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace();

        private static void ConfigureServices()
        {
            var services = new ServiceCollection();
            var factory = new DefaultServiceProviderFactory();
            
            services.AddViewModels();
            var provider = services.BuildServiceProvider();

            Services.Default = provider;
        }
    }
}
