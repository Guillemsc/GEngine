using System.Numerics;
using GEngine.Utils.Extensions;
using Raylib_cs;

namespace GEngine.Modules.Cameras.Objects;

public sealed class Camera2d
{
    public Color ClearColor { get; private set; } = Color.WHITE;
    public float Size { get; private set; }
    
    Camera2D _camera = new(
        Vector2.Zero,
        Vector2.Zero,
        0f,
        1f
    );

    public void SetOffset(Vector2 offset)
    {
        _camera.Offset = offset;
    }

    public void SetTarget(Vector2 target)
    {
        _camera.Target = target;
    }

    public void SetRotation(float rotation)
    {
        _camera.Rotation = rotation;
    }

    public void SetSize(float size)
    {
        Size = size;
        _camera.Zoom = MathExtensions.Divide(1f, Size);
    }

    public void SetClearColor(Color color)
    {
        ClearColor = color;
    }

    public Camera2D GetCameraDescriptor()
    {
        return _camera;
    }
}