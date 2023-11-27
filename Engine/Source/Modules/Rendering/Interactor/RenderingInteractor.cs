using GEngine.Modules.Renderer2d.UseCases;
using GEngine.Modules.Rendering.UseCases;

namespace GEngine.Modules.Rendering.Interactor;

public sealed class RenderingInteractor : IRenderingInteractor
{
    readonly AddToRendering2dQueueUseCase _addToRendering2dQueueUseCase;

    public RenderingInteractor(
        AddToRendering2dQueueUseCase addToRendering2dQueueUseCase
        )
    {
        _addToRendering2dQueueUseCase = addToRendering2dQueueUseCase;
    }

    public void AddToRendering2dQueue(int sortingOrder, Action action)
        => _addToRendering2dQueueUseCase.Execute(sortingOrder, action);
}