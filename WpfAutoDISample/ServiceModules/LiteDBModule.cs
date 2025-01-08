using System.IO;
using EasilyNET.AutoDependencyInjection.Contexts;
using EasilyNET.AutoDependencyInjection.Modules;
using LiteDB;
using LiteDB.Engine;
using Microsoft.Extensions.DependencyInjection;
using WpfAutoDISample.Common;

namespace WpfAutoDISample.ServiceModules;

internal sealed class LiteDbModule : AppModule
{
    public override async Task ConfigureServices(ConfigureServicesContext context)
    {
        var cacheDb = Path.Combine(Constant.GetUserDataPath(), "cache");
        // 确保数据目录存在
        if (!Directory.Exists(cacheDb)) Directory.CreateDirectory(cacheDb);
        context.Services.AddKeyedSingleton<ILiteDatabase>(Constant.AppCacheServiceKey, (_, _) => new LiteDatabase(new LiteEngine(new EngineSettings
        {
            AutoRebuild = true,
            Filename = Path.Combine(cacheDb, Constant.AppCacheDb)
        })));
        var uiDb = Path.Combine(Constant.GetUserDataPath(), "ui");
        // 确保数据目录存在
        if (!Directory.Exists(uiDb)) Directory.CreateDirectory(uiDb);
        context.Services.AddKeyedSingleton<ILiteDatabase>(Constant.UiConfigServiceKey, (_, _) => new LiteDatabase(new LiteEngine(new EngineSettings
        {
            AutoRebuild = true,
            Filename = Path.Combine(uiDb, Constant.UiConfigDb)
        })));
        await Task.CompletedTask;
    }
}