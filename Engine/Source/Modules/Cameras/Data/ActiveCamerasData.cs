using GEngine.Modules.Cameras.Objects;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Cameras.Data;

public sealed class ActiveCamerasData
{
    public Camera2d DefaultCamera2d;

    public Optional<Camera2d> Active2dCamera;
}