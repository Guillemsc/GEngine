using GEngine.Modules.Logging.UseCases;
using GEngine.Utils.Logging.Enums;
using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public class CloseWindowUseCase
{
    readonly GetLoggerUseCase _getLoggerUseCase;

    public CloseWindowUseCase(
        GetLoggerUseCase getLoggerUseCase
    )
    {
        _getLoggerUseCase = getLoggerUseCase;
    }
    
    public void Execute()
    {
        bool isWindowReady = Raylib.IsWindowReady();

        if (!isWindowReady)
        {
            return;
        }

        _getLoggerUseCase.Execute().Log(LogType.Info, "Closing window");
        
        Raylib.CloseWindow();
    }
}