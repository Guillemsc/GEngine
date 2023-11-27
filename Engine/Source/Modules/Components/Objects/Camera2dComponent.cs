using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Utils.Extensions;
using Raylib_cs;

namespace GEngine.Modules.Components.Objects;

public sealed class Camera2dComponent : Component
{
    public Camera2d Camera2d { get; } = new();
    
    public Color ClearColor { get; private set; } = Color.WHITE;
    public float Size { get; private set; } = 1f;
    
    public Camera2dComponent(IREngineInteractor engine, Guid uid, Entity owner) : base(engine, uid, owner)
    {
        
    }

    public override void Enable()
    {
        Engine.Cameras.SetActiveCamera2dIfThereIsNone(Camera2d);
    }

    public override void Tick()
    {
        float width = Raylib.GetScreenWidth();       
        float height = Raylib.GetScreenHeight();
        
        Vector2 cameraPosition = Owner.Transform.WorldPosition.ToVector2XY();
        
        Camera2d.SetOffset(new Vector2(width * 0.5f, height * 0.5f));
        Camera2d.SetTarget( cameraPosition with { Y = -cameraPosition.Y });
        Camera2d.SetSize(Size);
        Camera2d.SetClearColor(ClearColor);
    }

    public override void Disable()
    {
        Engine.Cameras.RemoveActiveCamera2dIfMatches(Camera2d);
    }

    public void SetClearColor(Color color)
    {
        ClearColor = color;
    }

    public void SetSize(float size)
    {
        Size = size;
    }

    public void AddSize(float size)
    {
        SetSize(Size + size);
    }

    public void FitBoundsToScreen(Vector2 bounds)
    {
        Vector2 screenSize = Engine.Windows.GetScreenSize();
        
        FitBoundsToSize(bounds, screenSize);
    }
    
    public void FitBoundsToSize(Vector2 bounds, Vector2 size)
    {
        float sizeHorizontalAspect = MathExtensions.Divide(bounds.X, size.X);
        float sizeVerticalAspect = MathExtensions.Divide(bounds.Y, size.Y);

        float finalSize = Math.Max(sizeVerticalAspect, sizeHorizontalAspect);
        
        SetSize(finalSize);
    }
}