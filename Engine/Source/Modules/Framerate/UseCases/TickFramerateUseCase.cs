namespace GEngine.Modules.Framerate.UseCases;

public sealed class TickFramerateUseCase
{
    readonly TickSecondAverageFpsUseCase _tickSecondAverageFpsUseCase;

    public TickFramerateUseCase(
        TickSecondAverageFpsUseCase tickSecondAverageFpsUseCase
        )
    {
        _tickSecondAverageFpsUseCase = tickSecondAverageFpsUseCase;
    }

    public void Execute()
    {
        _tickSecondAverageFpsUseCase.Execute();
    }
}