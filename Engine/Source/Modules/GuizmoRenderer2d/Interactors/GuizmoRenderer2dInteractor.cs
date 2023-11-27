using System.Numerics;
using GEngine.Modules.GuizmoRenderer2d.UseCases;
using Raylib_cs;

namespace GEngine.Modules.GuizmoRenderer2d.Interactors;

public sealed class GuizmoRenderer2dInteractor : IGuizmoRenderer2dInteractor
{
    readonly DrawLineGuizmo2dUseCase _drawLineGuizmo2dUseCase;
    readonly DrawQuadGuizmo2dUseCase _drawQuadGuizmo2dUseCase;

    public GuizmoRenderer2dInteractor(
        DrawLineGuizmo2dUseCase drawLineGuizmo2dUseCase,
        DrawQuadGuizmo2dUseCase drawQuadGuizmo2dUseCase
    )
    {
        _drawLineGuizmo2dUseCase = drawLineGuizmo2dUseCase;
        _drawQuadGuizmo2dUseCase = drawQuadGuizmo2dUseCase;
    }

    public void DrawLine(Vector2 start, Vector2 end, Color color)
        => _drawLineGuizmo2dUseCase.Execute(start, end, color);

    public void DrawQuad(Vector2 center, Vector2 size, Color color) 
        => _drawQuadGuizmo2dUseCase.Execute(center, size, color);
}