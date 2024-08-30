using System.Windows;
using EasilyNET.AutoDependencyInjection.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WpfAutoDISample.ViewModels;

namespace WpfAutoDISample.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[DependencyInjection(ServiceLifetime.Singleton, AddSelf = true)]
public partial class MainWindow
{
    private readonly ILogger<MainWindow> _logger;

    /// <inheritdoc />
    public MainWindow(MainWindowModel mwm, ILogger<MainWindow> logger)
    {
        InitializeComponent();
        DataContext = mwm;
        _logger = logger;
    }

    private async void OnBtnClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowModel vm) return;
        vm.Message = "Clicked";
        _logger.LogInformation("Clicked");
        await Task.CompletedTask;
    }
}