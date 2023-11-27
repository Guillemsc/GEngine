using rlImGui_cs;

namespace GEngine.Modules.EditorRenderer.UseCases;

public sealed class DisposeEditorRendererUseCase
{
    public void Execute()
    {
        rlImGui.Shutdown();
    }
}