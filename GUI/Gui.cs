using Avalonia;
using Avalonia.ReactiveUI;
using System;

namespace GUI;

public static class Gui
{
    public static void Main(string[] args)
    {
        Start();
    }

    [STAThread]
    private static void Start() => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(Array.Empty<string>());

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();
}