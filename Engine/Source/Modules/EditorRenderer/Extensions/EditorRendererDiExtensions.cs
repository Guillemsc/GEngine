using GEngine.Modules.EditorRenderer.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.EditorRenderer.Extensions;

public static class EditorRendererDiExtensions
{
    public static void LinkEditorRenderer<T>(
        this IDiBindingActionBuilder<T> actionBuilder,
        Func<T, Action> func
    )
    {
        actionBuilder.WhenInit((c, o) =>
                {
                    Action action = func.Invoke(o);
                    AddEditorRendererActionUseCase useCase = c.Resolve<AddEditorRendererActionUseCase>();
                    useCase.Execute(action);
                }
            )
            .NonLazy();
    }
}