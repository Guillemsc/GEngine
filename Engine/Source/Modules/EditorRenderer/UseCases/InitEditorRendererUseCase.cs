using GEngine.Modules.EditorRenderer.Data;
using rlImGui_cs;

namespace GEngine.Modules.EditorRenderer.UseCases;

public sealed class InitEditorRendererUseCase
{
    readonly EditorRendererFontData _editorRendererFontData;

    public InitEditorRendererUseCase(EditorRendererFontData editorRendererFontData)
    {
        _editorRendererFontData = editorRendererFontData;
    }

    public void Execute()
    {
        rlImGui.Setup(true);
    }
}