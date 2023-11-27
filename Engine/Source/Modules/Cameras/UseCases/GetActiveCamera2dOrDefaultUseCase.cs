using System.Numerics;
using GEngine.Modules.Cameras.Data;
using GEngine.Modules.Cameras.Objects;
using Raylib_cs;

namespace GEngine.Modules.Cameras.UseCases;

public sealed class GetActiveCamera2dOrDefaultUseCase
{
    readonly ActiveCamerasData _activeCamerasData;

    public GetActiveCamera2dOrDefaultUseCase(
        ActiveCamerasData activeCamerasData
        )
    {
        _activeCamerasData = activeCamerasData;
    }

    public Camera2d Execute()
    {
        bool hasActiveCamera = _activeCamerasData.Active2dCamera.TryGet(out Camera2d activeCamera);

        if (!hasActiveCamera)
        {
            float width = Raylib.GetScreenWidth();       
            float height = Raylib.GetScreenHeight();
        
            _activeCamerasData.DefaultCamera2d.SetOffset(new Vector2(width * 0.5f, height * 0.5f));
        
            return _activeCamerasData.DefaultCamera2d;   
        }

        return activeCamera;
    }
}