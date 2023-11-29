using System.Numerics;
using GEngine.Modules.Windows.Data;
using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class TickWindowSizeChangeCheckUseCase
{
    readonly WindowSizeData _windowSizeData;
    readonly WindowEventsData _windowEventsData;

    public TickWindowSizeChangeCheckUseCase(
        WindowSizeData windowSizeData, 
        WindowEventsData windowEventsData
        )
    {
        _windowSizeData = windowSizeData;
        _windowEventsData = windowEventsData;
    }

    public void Execute()
    {
        float screenWidth = Raylib.GetScreenWidth();       
        float screenHeight = Raylib.GetScreenHeight();
        
        Vector2 currentSize = new Vector2(screenWidth, screenHeight);

        if (currentSize == _windowSizeData.WindowSize)
        {
            return;
        }

        _windowSizeData.WindowSize = currentSize;
        _windowEventsData.WindowSizeChangedEvent.Raise(currentSize);
    }
}