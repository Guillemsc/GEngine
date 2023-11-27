using GEngine.Utils.Comparers;

namespace GEngine.Modules.Renderer2d.Data;

public sealed class Rendering2dQueuesData
{
    public SortedList<int, Action> Rendering2dQueue { get; } = new(new DuplicateKeyComparer<int>());
}