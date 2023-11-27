using GEngine.Modules.GuizmoRenderer2d.Data;

namespace GEngine.Modules.GuizmoRenderer2d.UseCases;

public sealed class AddToGuizmoRendering2dQueueUseCase
{
    readonly GuizmoRenderer2dRenderingQueueData _guizmoRenderer2dRenderingQueueData;

    public AddToGuizmoRendering2dQueueUseCase(
        GuizmoRenderer2dRenderingQueueData guizmoRenderer2dRenderingQueueData
    )
    {
        _guizmoRenderer2dRenderingQueueData = guizmoRenderer2dRenderingQueueData;
    }
    
    public void Execute(Action action)
    {
        _guizmoRenderer2dRenderingQueueData.RenderQueue.Add(action);
    }
}