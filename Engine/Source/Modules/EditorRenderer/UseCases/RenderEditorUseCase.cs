using GEngine.Modules.EditorRenderer.Data;
using GEngine.Modules.Modes.Enums;
using GEngine.Modules.Modes.UseCases;
using Raylib_cs;
using rlImGui_cs;

namespace GEngine.Modules.EditorRenderer.UseCases;

public sealed class RenderEditorUseCase
{
    readonly EditorRendererFontData _editorRendererFontData;
    readonly EditorRendererRenderListData _editorRendererRenderListData;
    readonly GetEngineModeUseCase _getEngineModeUseCase;

    public RenderEditorUseCase(
        EditorRendererFontData editorRendererFontData, 
        EditorRendererRenderListData editorRendererRenderListData, 
        GetEngineModeUseCase getEngineModeUseCase
        )
    {
        _editorRendererFontData = editorRendererFontData;
        _editorRendererRenderListData = editorRendererRenderListData;
        _getEngineModeUseCase = getEngineModeUseCase;
    }
    
    public void Execute()
    {
        if (_getEngineModeUseCase.Execute() == EngineModeType.Release)
        {
            return;
        }
        
        rlImGui.Begin(Raylib.GetFrameTime());

        for (int i = _editorRendererRenderListData.RenderList.Count - 1; i >= 0; --i)
        {
            Action renderAction = _editorRendererRenderListData.RenderList[i];
            renderAction.Invoke();
        }
        
        rlImGui.End();
    }
}