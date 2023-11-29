using System.Numerics;
using GEngine.Modules.Logging.UseCases;
using GEngine.Modules.Windows.Data;
using GEngine.Utils.Logging.Enums;
using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class InitWindowUseCase
{
    readonly WindowSizeData _windowSizeData;
    readonly GetLoggerUseCase _getLoggerUseCase;

    public InitWindowUseCase(
        WindowSizeData windowSizeData,
        GetLoggerUseCase getLoggerUseCase
        )
    {
        _windowSizeData = windowSizeData;
        _getLoggerUseCase = getLoggerUseCase;
    }

    public void Execute(int width, int height, string title)
    {
        _getLoggerUseCase.Execute().Log(
            LogType.Info,
            $"Showing window. Size:{width}x{height} title:{title}"
        );
        
        Raylib.SetConfigFlags(ConfigFlags.FLAG_MSAA_4X_HINT);
        
        Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
        Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT);
        Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_ALWAYS_RUN);
        
        Raylib.InitWindow(width, height, title);
        
        float screenWidth = Raylib.GetScreenWidth();       
        float screenHeight = Raylib.GetScreenHeight();
        _windowSizeData.WindowSize = new Vector2(screenWidth, screenHeight);
    }
}