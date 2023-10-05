using System;
using Avalonia;
using Avalonia.ReactiveUI;

namespace GUI;

public static class Gui
{
    public static void Main(string[] args)
    {
        Start();
    }

    [STAThread]
    private static void Start()
    {
        BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(Array.Empty<string>());
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
    }
}