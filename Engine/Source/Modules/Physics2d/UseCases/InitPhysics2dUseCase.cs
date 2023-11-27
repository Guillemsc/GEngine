namespace GEngine.Modules.Physics2d.UseCases;

public sealed class InitPhysics2dUseCase
{
    readonly CreatePhysicsWorld2dUseCase _createPhysicsWorld2dUseCase;

    public InitPhysics2dUseCase(
        CreatePhysicsWorld2dUseCase createPhysicsWorld2dUseCase
        )
    {
        _createPhysicsWorld2dUseCase = createPhysicsWorld2dUseCase;
    }

    public void Execute()
    {
        _createPhysicsWorld2dUseCase.Execute();
    }
}