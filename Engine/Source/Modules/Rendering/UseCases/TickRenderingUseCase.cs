using Raylib_cs;

namespace GEngine.Modules.Rendering.UseCases;

public sealed class TickRenderingUseCase
{
    readonly RenderUseCase _renderUseCase;

    public TickRenderingUseCase(
        RenderUseCase renderUseCase
        )
    {
        _renderUseCase = renderUseCase;
    }

    public void Execute()
    {
        _renderUseCase.Execute();
    }
}