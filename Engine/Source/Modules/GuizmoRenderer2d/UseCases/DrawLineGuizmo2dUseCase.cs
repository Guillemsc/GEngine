using System.Numerics;
using Raylib_cs;

namespace GEngine.Modules.GuizmoRenderer2d.UseCases;

public sealed class DrawLineGuizmo2dUseCase
{
    readonly AddToGuizmoRendering2dQueueUseCase _addToGuizmoRendering2dQueueUseCase;

    public DrawLineGuizmo2dUseCase(
        AddToGuizmoRendering2dQueueUseCase addToGuizmoRendering2dQueueUseCase
    )
    {
        _addToGuizmoRendering2dQueueUseCase = addToGuizmoRendering2dQueueUseCase;
    }

    public void Execute(Vector2 start, Vector2 end, Color color)
    {
        void Render() => Raylib.DrawLineV(
            start with { Y = -start.Y },
            end with { Y = -end.Y },
            color
        ); 
        
        _addToGuizmoRendering2dQueueUseCase.Execute(Render);
    }
}