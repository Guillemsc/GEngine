using GEngine.Modules.GuizmoRenderer2d.Data;
using GEngine.Modules.Physics2d.UseCases;

namespace GEngine.Modules.GuizmoRenderer2d.UseCases;

public sealed class RenderGuizmos2dUseCase
{
    readonly DrawAllPhysicsWithGuizmosUseCase _drawAllPhysicsWithGuizmosUseCase;
    readonly RenderGuizmos2dRenderQueueUseCase _renderGuizmos2dRenderQueueUseCase;

    public RenderGuizmos2dUseCase(
        DrawAllPhysicsWithGuizmosUseCase drawAllPhysicsWithGuizmosUseCase,
        RenderGuizmos2dRenderQueueUseCase renderGuizmos2dRenderQueueUseCase
    )
    {
        _drawAllPhysicsWithGuizmosUseCase = drawAllPhysicsWithGuizmosUseCase;
        _renderGuizmos2dRenderQueueUseCase = renderGuizmos2dRenderQueueUseCase;
    }

    public void Execute()
    {
        _drawAllPhysicsWithGuizmosUseCase.Execute();
        _renderGuizmos2dRenderQueueUseCase.Execute();
    }
}