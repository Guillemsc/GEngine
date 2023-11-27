using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Tickables.Interactors;

public interface ITickablesInteractor
{
    void AddTickable(ITickable tickable, TickType tickType = TickType.Update);
    void RemoveTickable(ITickable tickable, TickType tickType = TickType.Update);

    void Tick();
}