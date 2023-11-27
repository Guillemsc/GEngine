using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Tickables.Data
{
    public sealed class TickablesData
    {
        public TickablesContainerTickable PreUpdateTickables { get; } = new();
        public TickablesContainerTickable UpdateTickables { get; } = new();
        public TickablesContainerTickable LateUpdateTickables { get; } = new();
    }
}
