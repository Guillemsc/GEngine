using System.Numerics;
using GEngine.Utils.Events;

namespace GEngine.Modules.Windows.Data;

public sealed class WindowEventsData
{
    public IEvent<Vector2> WindowSizeChangedEvent { get; } = new Event<Vector2>();
}