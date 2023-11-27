using GEngine.Modules.Logging.UseCases;
using GEngine.Utils.Logging.Enums;
using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class InitWindowUseCase
{
    readonly GetLoggerUseCase _getLoggerUseCase;

    public InitWindowUseCase(
        GetLoggerUseCase getLoggerUseCase
        )
    {
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
    }
}