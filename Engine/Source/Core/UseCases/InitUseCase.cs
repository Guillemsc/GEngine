using GEngine.Modules.Cameras.UseCases;
using GEngine.Modules.EditorRenderer.UseCases;
using GEngine.Modules.Logging.UseCases;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Modules.Rendering.UseCases;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Logging.Enums;

namespace GEngine.Core.UseCases;

public sealed class InitUseCase
{
    readonly GetLoggerUseCase _getLoggerUseCase;
    readonly InitWindowUseCase _initWindowUseCase;
    readonly InitCamerasUseCase _initCamerasUseCase;
    readonly InitRenderingUseCase _initRenderingUseCase;
    readonly InitEditorRendererUseCase _initEditorRendererUseCase;
    readonly InitPhysics2dUseCase _initPhysics2dUseCase;

    public InitUseCase(
        GetLoggerUseCase getLoggerUseCase,
        InitWindowUseCase initWindowUseCase,
        InitCamerasUseCase initCamerasUseCase,
        InitRenderingUseCase initRenderingUseCase,
        InitEditorRendererUseCase initEditorRendererUseCase,
        InitPhysics2dUseCase initPhysics2dUseCase
        )
    {
        _getLoggerUseCase = getLoggerUseCase;
        _initWindowUseCase = initWindowUseCase;
        _initCamerasUseCase = initCamerasUseCase;
        _initRenderingUseCase = initRenderingUseCase;
        _initEditorRendererUseCase = initEditorRendererUseCase;
        _initPhysics2dUseCase = initPhysics2dUseCase;
    }

    public void Execute()
    {
        _getLoggerUseCase.Execute().Log(LogType.Info, "Initialising GEngine");
        
        _initWindowUseCase.Execute(1280, 800, "Hello World");
        _initCamerasUseCase.Execute();
        _initRenderingUseCase.Execute();
        _initEditorRendererUseCase.Execute();
        _initPhysics2dUseCase.Execute();
        
        _getLoggerUseCase.Execute().Log(LogType.Info, "GEngine initialised");
    }
}