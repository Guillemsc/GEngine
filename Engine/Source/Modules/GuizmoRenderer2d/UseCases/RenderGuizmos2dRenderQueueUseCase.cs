using GEngine.Modules.GuizmoRenderer2d.Data;

namespace GEngine.Modules.GuizmoRenderer2d.UseCases;

public sealed class RenderGuizmos2dRenderQueueUseCase
{
    readonly GuizmoRenderer2dRenderingQueueData _guizmoRenderer2dRenderingQueueData;

    public RenderGuizmos2dRenderQueueUseCase(
        GuizmoRenderer2dRenderingQueueData guizmoRenderer2dRenderingQueueData
    )
    {
        _guizmoRenderer2dRenderingQueueData = guizmoRenderer2dRenderingQueueData;
    }

    public void Execute()
    {
        foreach (Action action in _guizmoRenderer2dRenderingQueueData.RenderQueue)
        {
            action.Invoke();
        }
        
        _guizmoRenderer2dRenderingQueueData.RenderQueue.Clear();
    }
}