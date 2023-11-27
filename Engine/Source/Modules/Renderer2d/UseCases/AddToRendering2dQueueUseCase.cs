using GEngine.Modules.Renderer2d.Data;

namespace GEngine.Modules.Renderer2d.UseCases;

public sealed class AddToRendering2dQueueUseCase
{
    readonly Rendering2dQueuesData _rendering2dQueuesData;

    public AddToRendering2dQueueUseCase(
        Rendering2dQueuesData rendering2dQueuesData
        )
    {
        _rendering2dQueuesData = rendering2dQueuesData;
    }

    public void Execute(int sortingOrder, Action action)
    {
        _rendering2dQueuesData.Rendering2dQueue.Add(sortingOrder, action);
    }
}