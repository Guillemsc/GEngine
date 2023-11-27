using GEngine.Modules.EditorRenderer.UseCases;
using GEngine.Modules.Logging.UseCases;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Logging.Enums;

namespace GEngine.Core.UseCases;

public sealed class DisposeUseCase
{
    readonly GetLoggerUseCase _getLoggerUseCase;
    readonly DisposeEditorRendererUseCase _disposeEditorRendererUseCase;
    readonly CloseWindowUseCase _closeWindowUseCase;

    public DisposeUseCase(
        GetLoggerUseCase getLoggerUseCase, 
        DisposeEditorRendererUseCase disposeEditorRendererUseCase,
        CloseWindowUseCase closeWindowUseCase
        )
    {
        _getLoggerUseCase = getLoggerUseCase;
        _disposeEditorRendererUseCase = disposeEditorRendererUseCase;
        _closeWindowUseCase = closeWindowUseCase;
    }
    
    public void Execute()
    {
        _getLoggerUseCase.Execute().Log(LogType.Info, "Disposing GEngine");
        
        _disposeEditorRendererUseCase.Execute();
        _closeWindowUseCase.Execute();
        
        _getLoggerUseCase.Execute().Log(LogType.Info, "GEngine disposed");
    }
}