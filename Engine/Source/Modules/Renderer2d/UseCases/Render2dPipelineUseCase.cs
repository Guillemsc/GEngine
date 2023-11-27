using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Cameras.UseCases;
using GEngine.Modules.GuizmoRenderer2d.UseCases;
using GEngine.Modules.Modes.Enums;
using GEngine.Modules.Modes.UseCases;
using GEngine.Modules.Renderer2d.UseCases;
using Raylib_cs;

namespace GEngine.Modules.Rendering.UseCases;

public sealed class Render2dPipelineUseCase
{
    readonly GetEngineModeUseCase _getEngineModeUseCase;
    readonly GetActiveCamera2dOrDefaultUseCase _getActiveCamera2dOrDefaultUseCase;
    readonly RenderRendering2dQueueUseCase _renderRendering2dQueueUseCase;
    readonly RenderGuizmos2dUseCase _renderGuizmos2dUseCase;

    public Render2dPipelineUseCase(
        GetEngineModeUseCase getEngineModeUseCase,
        GetActiveCamera2dOrDefaultUseCase getActiveCamera2dOrDefaultUseCase,
        RenderRendering2dQueueUseCase renderRendering2dQueueUseCase,
        RenderGuizmos2dUseCase renderGuizmos2dUseCase
        )
    {
        _getEngineModeUseCase = getEngineModeUseCase;
        _getActiveCamera2dOrDefaultUseCase = getActiveCamera2dOrDefaultUseCase;
        _renderRendering2dQueueUseCase = renderRendering2dQueueUseCase;
        _renderGuizmos2dUseCase = renderGuizmos2dUseCase;
    }

    public void Execute()
    {
        Camera2d camera2d = _getActiveCamera2dOrDefaultUseCase.Execute();
        
        Raylib.ClearBackground(camera2d.ClearColor);
        
        Raylib.BeginMode2D(camera2d.GetCameraDescriptor());
        
        _renderRendering2dQueueUseCase.Execute();

        if (_getEngineModeUseCase.Execute() != EngineModeType.Release)
        {
            _renderGuizmos2dUseCase.Execute();
        }
        
        Raylib.EndMode2D();
    }
}