using GEngine.Utils.Comparers;

namespace GEngine.Modules.UiRenderer.Data;

public sealed class UiRenderingQueuesData
{
    public SortedList<int, Action<float>> UiRenderingQueue { get; } = new(new DuplicateKeyComparer<int>());
}