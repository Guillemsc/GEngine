using GEngine.Modules.Tickables.Data;

namespace GEngine.Modules.Tickables.UseCases;

public sealed class RemoveAllTickablesUseCase
{
    readonly TickablesData _tickablesData;

    public RemoveAllTickablesUseCase(
        TickablesData tickablesData
    )
    {
        _tickablesData = tickablesData;
    }
    
    public void Execute()
    {
        _tickablesData.PreUpdateTickables.Clear();
        _tickablesData.UpdateTickables.Clear();
        _tickablesData.LateUpdateTickables.Clear();
    }
}