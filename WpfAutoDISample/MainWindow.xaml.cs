using EasilyNET.AutoDependencyInjection.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfAutoDISample.ViewModels;

namespace WpfAutoDISample;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
[DependencyInjection(ServiceLifetime.Singleton, AddSelf = true)]
public partial class MainWindow
{
    private readonly MainWindowModel _model;

    /// <inheritdoc />
    public MainWindow(MainWindowModel mwm)
    {
        InitializeComponent();
        _model = mwm;
        DataContext = mwm;
    }

    private async void OnBtnClick(object sender, RoutedEventArgs e)
    {
        _model.Message = "Clicked";
        await Task.CompletedTask;
    }
}