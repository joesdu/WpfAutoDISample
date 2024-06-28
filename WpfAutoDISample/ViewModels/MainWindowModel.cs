using CommunityToolkit.Mvvm.ComponentModel;
using EasilyNET.AutoDependencyInjection.Core.Attributes;
using Microsoft.Extensions.DependencyInjection;

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable ClassNeverInstantiated.Global

namespace WpfAutoDISample.ViewModels;

/// <inheritdoc cref="ObservableObject" />
[DependencyInjection(ServiceLifetime.Singleton, AddSelf = true, SelfOnly = true)]
public partial class MainWindowModel : ObservableObject
{
    /// <summary>
    /// Message
    /// </summary>
    [ObservableProperty]
    private string message = "Hello WPF!";
}

// 使用接口注入的例子
//public partial class MainWindowModel : ObservableObject, ISingletonDependency
//{
//    /// <summary>
//    /// Message
//    /// </summary>
//    [ObservableProperty]
//    private string message = "Hello WPF!";

//    // ReSharper disable once UnusedMember.Global
//    public static bool? DependencyInjectionSelf => true;

//    // ReSharper disable once UnusedMember.Global
//    public static bool? DependencyInjectionSelfOnly => true;
//}