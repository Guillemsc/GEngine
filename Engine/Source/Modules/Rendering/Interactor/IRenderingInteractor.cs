namespace GEngine.Modules.Rendering.Interactor;

public interface IRenderingInteractor
{
    void AddToRendering2dQueue(int sortingOrder, Action action);
}