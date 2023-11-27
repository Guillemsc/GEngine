using GEngine.Modules.Tickables.Data;
using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Tickables.UseCases
{
    public sealed class RemoveTickableUseCase
    {
        readonly TickablesData _tickablesData;

        public RemoveTickableUseCase(
            TickablesData tickablesData
            )
        {
            _tickablesData = tickablesData;
        }

        public void Execute(ITickable tickable, TickType tickType = TickType.Update)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                    {
                        _tickablesData.PreUpdateTickables.Remove(tickable);
                        break;
                    }

                default:
                case TickType.Update:
                    {
                        _tickablesData.UpdateTickables.Remove(tickable);
                        break;
                    }

                case TickType.LateUpdate:
                    {
                        _tickablesData.LateUpdateTickables.Remove(tickable);
                        break;
                    }
            }
        }
    }
}
