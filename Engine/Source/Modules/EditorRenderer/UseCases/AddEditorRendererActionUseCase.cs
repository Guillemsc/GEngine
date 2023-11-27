using GEngine.Modules.EditorRenderer.Data;

namespace GEngine.Modules.EditorRenderer.UseCases;

public sealed class AddEditorRendererActionUseCase
{
    readonly EditorRendererRenderListData _editorRendererRenderListData;

    public AddEditorRendererActionUseCase(
        EditorRendererRenderListData editorRendererRenderListData
        )
    {
        _editorRendererRenderListData = editorRendererRenderListData;
    }

    public void Execute(Action action)
    {
        _editorRendererRenderListData.RenderList.Add(action);
    }
}