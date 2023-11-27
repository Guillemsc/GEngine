using System.Numerics;
using Raylib_cs;

namespace GEngine.Modules.GuizmoRenderer2d.UseCases;

public sealed class DrawQuadGuizmo2dUseCase
{
    readonly AddToGuizmoRendering2dQueueUseCase _addToGuizmoRendering2dQueueUseCase;

    public DrawQuadGuizmo2dUseCase(
        AddToGuizmoRendering2dQueueUseCase addToGuizmoRendering2dQueueUseCase
    )
    {
        _addToGuizmoRendering2dQueueUseCase = addToGuizmoRendering2dQueueUseCase;
    }

    public void Execute(Vector2 center, Vector2 size, Color color)
    {
        Vector2 halfSize = size * 0.5f;
        Vector2 actualCenter = new Vector2(
            center.X - halfSize.X,
            center.Y + halfSize.Y
        );
        
        void Render() => Raylib.DrawRectangleLines(
            (int)actualCenter.X ,
            (int)-actualCenter.Y,
            (int)size.X,
            (int)size.Y,
            color
        ); 
        
        _addToGuizmoRendering2dQueueUseCase.Execute(Render);
    }
}