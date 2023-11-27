namespace GEngine.Modules.Entities.UseCases;

public sealed class TickEntitiesUseCase
{
    readonly TickActiveEntitiesUseCase _tickActiveEntitiesUseCase;

    public TickEntitiesUseCase(
        TickActiveEntitiesUseCase tickActiveEntitiesUseCase
        )
    {
        _tickActiveEntitiesUseCase = tickActiveEntitiesUseCase;
    }

    public void Execute()
    {
        _tickActiveEntitiesUseCase.Execute();
    }
}