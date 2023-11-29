using System.Numerics;
using GEngine.Modules.Cameras.Data;
using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Windows.UseCases;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class GetActiveCamera2dOrDefaultUseCase
{
    readonly ActiveCamerasData _activeCamerasData;
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;

    public GetActiveCamera2dOrDefaultUseCase(
        ActiveCamerasData activeCamerasData, 
        GetWindowSizeUseCase getWindowSizeUseCase
        )
    {
        _activeCamerasData = activeCamerasData;
        _getWindowSizeUseCase = getWindowSizeUseCase;
    }

    public Camera2d Execute()
    {
        bool hasActiveCamera = _activeCamerasData.Active2dCamera.TryGet(out Camera2d activeCamera);

        if (!hasActiveCamera)
        {
            Vector2 windowSize = _getWindowSizeUseCase.Execute();
        
            _activeCamerasData.DefaultCamera2d.SetOffset(new Vector2(windowSize.X * 0.5f, windowSize.Y * 0.5f));
        
            return _activeCamerasData.DefaultCamera2d;   
        }

        return activeCamera;
    }
}