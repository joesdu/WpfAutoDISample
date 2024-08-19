using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WpfAutoDISample;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    private IHost AppHost { get; set; }

    /// <summary>
    /// Services
    /// </summary>
    public static IServiceProvider Services => CurrentAppHost.Services;

    /// <summary>
    /// 获取当前AppHost
    /// </summary>
    private static IHost CurrentAppHost => (Current as App)?.AppHost ?? throw new InvalidOperationException("无法获取AppHost，当前Application实例不是App类型。");

    /// <summary>
    /// Main入口函数
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    [STAThread]
    public static void Main(string[] args)
    {
        // 创建一个通用主机
        using var host = CreateHostBuilder(args).Build();
        host.InitializeApplication();
        // 配置主窗口
        var app = new App();
        app.InitializeComponent();
        app.AppHost = host;
        app.MainWindow = CurrentAppHost.Services.GetRequiredService<MainWindow>();
        app.MainWindow.Visibility = Visibility.Visible;
        app.Run();
    }

    private static IHostBuilder CreateHostBuilder(params string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(AppContext.BaseDirectory);
                c.AddJsonFile("appsettings.json", false, false);
            })
            .ConfigureServices(sc =>
            {
                sc.AddApplicationModules<AppServiceModules>();
                sc.AddSingleton<WeakReferenceMessenger>();
                sc.AddSingleton<IMessenger, WeakReferenceMessenger>(provider => provider.GetRequiredService<WeakReferenceMessenger>());
                sc.AddSingleton(_ => Current.Dispatcher);
            });

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync().ConfigureAwait(false);
        AppHost.Dispose();
        Shutdown();
        base.OnExit(e);
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync().ConfigureAwait(false);
        base.OnStartup(e);
    }
}