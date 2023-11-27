using GEngine.Modules.EditorRenderer.UseCases;
using Raylib_cs;
using GEngine.Modules.GuizmoRenderer2d.UseCases;

namespace GEngine.Modules.Rendering.UseCases;

public sealed class RenderUseCase
{
    readonly Render2dPipelineUseCase _render2dPipelineUseCase;
    readonly RenderEditorUseCase _renderEditorUseCase;

    public RenderUseCase(
        Render2dPipelineUseCase render2dPipelineUseCase, 
        RenderEditorUseCase renderEditorUseCase
    )
    {
        _render2dPipelineUseCase = render2dPipelineUseCase;
        _renderEditorUseCase = renderEditorUseCase;
    }
    
    public void Execute()
    {
        Raylib.BeginDrawing();
        
        _render2dPipelineUseCase.Execute();
        _renderEditorUseCase.Execute();

        Raylib.EndDrawing();
    }
}