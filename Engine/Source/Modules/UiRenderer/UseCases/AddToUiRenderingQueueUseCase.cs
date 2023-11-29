using GEngine.Modules.UiRenderer.Data;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class AddToUiRenderingQueueUseCase
{
    readonly UiRenderingQueuesData _uiRenderingQueuesData;

    public AddToUiRenderingQueueUseCase(UiRenderingQueuesData uiRenderingQueuesData)
    {
        _uiRenderingQueuesData = uiRenderingQueuesData;
    }
    
    public void Execute(int sortingOrder, Action<float> action)
    {
        _uiRenderingQueuesData.UiRenderingQueue.Add(sortingOrder, action);
    }
}