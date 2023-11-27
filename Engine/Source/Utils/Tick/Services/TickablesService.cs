using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Utils.Tick.Services
{
    /// <inheritdoc cref="ITickablesService" />
    public sealed class TickablesService : ITickablesService
    {
        readonly TickablesContainerTickable _preUpdateTickables = new();
        readonly TickablesContainerTickable _updateTickables = new();
        readonly TickablesContainerTickable _lateUpdateTickables = new();

        void Update()
        {
            _preUpdateTickables.Tick();
            _updateTickables.Tick();
            _lateUpdateTickables.Tick();
        }
        
        public void Add(ITickable tickable, TickType tickType)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                {
                    _preUpdateTickables.Add(tickable);
                    break;
                }

                default:
                case TickType.Update:
                {
                    _updateTickables.Add(tickable);
                    break;
                }

                case TickType.LateUpdate:
                {
                    _lateUpdateTickables.Add(tickable);
                    break;
                }
            }
        }

        public void Remove(ITickable tickable, TickType tickType)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                {
                    _preUpdateTickables.Remove(tickable);
                    break;
                }

                default:
                case TickType.Update:
                {
                    _updateTickables.Remove(tickable);
                    break;
                }

                case TickType.LateUpdate:
                {
                    _lateUpdateTickables.Remove(tickable);
                    break;
                }
            }
        }

        public void RemoveNow(ITickable tickable, TickType tickType)
        {
            switch (tickType)
            {
                case TickType.PreUpdate:
                {
                    _preUpdateTickables.Remove(tickable);
                    _preUpdateTickables.ActuallyRemoveTickables();
                    break;
                }

                default:
                case TickType.Update:
                {
                    _updateTickables.Remove(tickable);
                    _updateTickables.ActuallyRemoveTickables();
                    break;
                }

                case TickType.LateUpdate:
                {
                    _lateUpdateTickables.Remove(tickable);
                    _lateUpdateTickables.ActuallyRemoveTickables();
                    break;
                }
            }
        }
    }
}
