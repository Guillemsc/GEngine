using GEngine.Modules.Tickables.Data;
using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Tickables.UseCases
{
    public sealed class AddTickableUseCase
    {
        readonly TickablesData _tickablesData;

        public AddTickableUseCase(
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
                        _tickablesData.PreUpdateTickables.Add(tickable);
                        break;
                    }

                default:
                case TickType.Update:
                    {
                        _tickablesData.UpdateTickables.Add(tickable);
                        break;
                    }

                case TickType.LateUpdate:
                    {
                        _tickablesData.LateUpdateTickables.Add(tickable);
                        break;
                    }
            }
        }
    }
}
