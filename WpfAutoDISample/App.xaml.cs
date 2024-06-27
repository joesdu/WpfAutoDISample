using CommunityToolkit.Mvvm.Messaging;
using iNKORE.UI.WPF.Modern;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace WpfAutoDISample;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private static IHost AppHost { get; set; } = null!;

    /// <summary>
    /// Main入口函数
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [STAThread]
    public static void Main(string[] args)
    {
        // 创建一个通用主机
        AppHost = CreateHostBuilder(args).Build();
        AppHost.InitializeApplication();
        AppHost.StartAsync().ConfigureAwait(true).GetAwaiter().GetResult();
        RunApp();
    }

    private static void RunApp()
    {
        var app = new App();
        app.InitializeComponent();
        app.MainWindow = AppHost.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(params string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(c =>
            {
                var basePath =
                    Path.GetDirectoryName(AppContext.BaseDirectory) ?? throw new DirectoryNotFoundException("Unable to find the base directory of the application.");
                _ = c.SetBasePath(basePath);
            })
            .ConfigureServices(sc =>
            {
                sc.AddApplicationModules<AppServiceModules>();
                sc.AddSingleton<WeakReferenceMessenger>();
                sc.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());
                sc.AddSingleton(_ => Current.Dispatcher);
            });

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;
        UpdateTheme();
    }

    private static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (e.Category == UserPreferenceCategory.General)
        {
            UpdateTheme();
        }
    }

    private static bool IsWindowsLightTheme()
    {
        var registryValue = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", "SystemUsesLightTheme", 1);
        return registryValue is 1;
    }

    private static void UpdateTheme()
    {
        ThemeManager.Current.ApplicationTheme = IsWindowsLightTheme() ? ApplicationTheme.Light : ApplicationTheme.Dark;
    }

    protected override void OnExit(ExitEventArgs e)
    {
        SystemEvents.UserPreferenceChanged -= SystemEvents_UserPreferenceChanged;
        AppHost.StopAsync().ConfigureAwait(true).GetAwaiter().GetResult();
        base.OnExit(e);
    }
}