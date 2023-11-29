namespace GEngine.Modules.Windows.UseCases;

public sealed class TickWindowsUseCase
{
    readonly TickWindowSizeChangeCheckUseCase _tickWindowSizeChangeCheckUseCase;

    public TickWindowsUseCase(
        TickWindowSizeChangeCheckUseCase tickWindowSizeChangeCheckUseCase
        )
    {
        _tickWindowSizeChangeCheckUseCase = tickWindowSizeChangeCheckUseCase;
    }

    public void Execute()
    {
        _tickWindowSizeChangeCheckUseCase.Execute();
    }
}