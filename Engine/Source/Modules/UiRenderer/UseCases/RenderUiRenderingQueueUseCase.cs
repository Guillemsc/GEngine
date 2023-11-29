using GEngine.Modules.UiRenderer.Data;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class RenderUiRenderingQueueUseCase
{
    readonly UiRenderingQueuesData _uiRenderingQueuesData;

    public RenderUiRenderingQueueUseCase(UiRenderingQueuesData uiRenderingQueuesData)
    {
        _uiRenderingQueuesData = uiRenderingQueuesData;
    }

    public void Execute(float scale)
    {
        foreach (KeyValuePair<int, Action<float>> element in _uiRenderingQueuesData.UiRenderingQueue)
        {
            element.Value.Invoke(scale);
        }
        
        _uiRenderingQueuesData.UiRenderingQueue.Clear();
    }
}