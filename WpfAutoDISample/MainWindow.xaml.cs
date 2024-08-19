using System.Windows;
using EasilyNET.AutoDependencyInjection.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using WpfAutoDISample.ViewModels;

namespace WpfAutoDISample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[DependencyInjection(ServiceLifetime.Singleton, AddSelf = true)]
public partial class MainWindow
{
    /// <inheritdoc />
    public MainWindow(MainWindowModel mwm)
    {
        InitializeComponent();
        DataContext = mwm;
    }

    private async void OnBtnClick(object sender, RoutedEventArgs e)
    {
        if (DataContext is not MainWindowModel vm) return;
        vm.Message = "Clicked";
        await Task.CompletedTask;
    }
}