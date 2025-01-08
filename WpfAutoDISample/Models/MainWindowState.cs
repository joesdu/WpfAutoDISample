using LiteDB;

// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace WpfAutoDISample.Models;

internal sealed class MainWindowState
{
    [BsonId]
    public required ObjectId Id { get; set; } // 使用固定ID，因为只存储一个窗口状态

    public double Width { get; init; }

    public double Height { get; init; }

    public double Top { get; init; }

    public double Left { get; init; }
}