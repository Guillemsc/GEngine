using GEngine.Modules.Resources.Objects;
using GEngine.Modules.Resources.UseCases;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Resources.Interactors;

public sealed class ResourcesInteractor : IResourcesInteractor
{
    readonly LoadFontResourceUseCase _loadFontResourceUseCase;

    public ResourcesInteractor(
        LoadFontResourceUseCase loadFontResourceUseCase
        )
    {
        _loadFontResourceUseCase = loadFontResourceUseCase;
    }
    
    public Optional<FontResource> LoadFontResource(string filePath)
        => _loadFontResourceUseCase.Execute(filePath);
}