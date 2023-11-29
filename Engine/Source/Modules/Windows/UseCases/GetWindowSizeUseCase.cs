using System.Numerics;
using GEngine.Modules.Windows.Data;
using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class GetWindowSizeUseCase
{
    readonly WindowSizeData _windowSizeData;

    public GetWindowSizeUseCase(
        WindowSizeData windowSizeData
        )
    {
        _windowSizeData = windowSizeData;
    }

    public Vector2 Execute()
    {
        return _windowSizeData.WindowSize;
    }
}