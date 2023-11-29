using System.Numerics;
using GEngine.Modules.Cameras.Objects;
using GEngine.Modules.Modes.Enums;
using GEngine.Modules.Windows.UseCases;
using Raylib_cs;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class RenderUiPipelineUseCase
{
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;
    readonly RenderUiRenderingQueueUseCase _renderUiRenderingQueueUseCase;
    readonly GetUiScaleUseCase _getUiScaleUseCase;

    public RenderUiPipelineUseCase(
        GetWindowSizeUseCase getWindowSizeUseCase,
        RenderUiRenderingQueueUseCase renderUiRenderingQueueUseCase, 
        GetUiScaleUseCase getUiScaleUseCase
        )
    {
        _getWindowSizeUseCase = getWindowSizeUseCase;
        _renderUiRenderingQueueUseCase = renderUiRenderingQueueUseCase;
        _getUiScaleUseCase = getUiScaleUseCase;
    }

    public void Execute()
    {
        Vector2 windowSize = _getWindowSizeUseCase.Execute();

        float renderScale = _getUiScaleUseCase.Execute();
        
        Camera2d camera2d = new Camera2d();
        camera2d.SetOffset(windowSize * 0.5f);
        
        Raylib.BeginMode2D(camera2d.GetCameraDescriptor());
        
        _renderUiRenderingQueueUseCase.Execute(renderScale);

        // Vector2 size = new Vector2(100, 100) * renderScale;
        //
        // Rectangle rectangle = new(
        //     0,
        //     0,
        //     size.X,
        //     size.Y
        // );
        //
        // Raylib.DrawRectanglePro(
        //     rectangle,
        //     size * 0.5f, 
        //     -0,
        //     Color.PINK
        // );
        //
        // Vector2 rectangle2Position = new Vector2(300, 0) * renderScale;
        //
        // Rectangle rectangle2 = new(
        //     rectangle2Position.X,
        //     rectangle2Position.Y,
        //     size.X,
        //     size.Y
        // );
        //
        // Raylib.DrawRectanglePro(
        //     rectangle2,
        //     size * 0.5f, 
        //     -0,
        //     Color.PINK
        // );
        
        Raylib.EndMode2D();
    }
}