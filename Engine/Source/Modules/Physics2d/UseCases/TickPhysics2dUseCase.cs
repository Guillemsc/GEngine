namespace GEngine.Modules.Physics2d.UseCases;

public sealed class TickPhysics2dUseCase
{
    readonly StepPhysics2dWorldUseCase _stepPhysics2dWorldUseCase;
    readonly ResolvePhysics2dContactCallbacksUseCase _resolvePhysics2dContactCallbacksUseCase;

    public TickPhysics2dUseCase(
        StepPhysics2dWorldUseCase stepPhysics2dWorldUseCase,
            ResolvePhysics2dContactCallbacksUseCase resolvePhysics2dContactCallbacksUseCase
    )
    {
        _stepPhysics2dWorldUseCase = stepPhysics2dWorldUseCase;
        _resolvePhysics2dContactCallbacksUseCase = resolvePhysics2dContactCallbacksUseCase;
    }
    
    public void Execute()
    {
        _stepPhysics2dWorldUseCase.Execute();
        _resolvePhysics2dContactCallbacksUseCase.Execute();
    }
}