using GEngine.Modules.Renderer2d.Data;

namespace GEngine.Modules.Renderer2d.UseCases;

public sealed class RenderRendering2dQueueUseCase
{
    readonly Rendering2dQueuesData _rendering2dQueuesData;

    public RenderRendering2dQueueUseCase(
        Rendering2dQueuesData rendering2dQueuesData
    )
    {
        _rendering2dQueuesData = rendering2dQueuesData;
    }

    public void Execute()
    {
        foreach (KeyValuePair<int, Action> element in _rendering2dQueuesData.Rendering2dQueue)
        {
            element.Value.Invoke();
        }
        
        _rendering2dQueuesData.Rendering2dQueue.Clear();
    }
}