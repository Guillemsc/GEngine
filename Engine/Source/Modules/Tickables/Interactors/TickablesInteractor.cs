using GEngine.Modules.Tickables.UseCases;
using GEngine.Utils.Tick.Enums;
using GEngine.Utils.Tick.Tickables;

namespace GEngine.Modules.Tickables.Interactors;

public sealed class TickablesInteractor : ITickablesInteractor
{
    readonly AddTickableUseCase _addTickableUseCase;
    readonly RemoveTickableUseCase _removeTickableUseCase;
    readonly TickUseCase _tickUseCase;

    public TickablesInteractor(
        AddTickableUseCase addTickableUseCase,
        RemoveTickableUseCase removeTickableUseCase,
        TickUseCase tickUseCase
        )
    {
        _addTickableUseCase = addTickableUseCase;
        _removeTickableUseCase = removeTickableUseCase;
        _tickUseCase = tickUseCase;
    }

    public void AddTickable(ITickable tickable, TickType tickType = TickType.Update)
        => _addTickableUseCase.Execute(tickable, tickType);

    public void RemoveTickable(ITickable tickable, TickType tickType = TickType.Update)
        => _removeTickableUseCase.Execute(tickable, tickType);

    public void Tick() => _tickUseCase.Execute();
}