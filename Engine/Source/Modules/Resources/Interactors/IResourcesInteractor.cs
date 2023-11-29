using GEngine.Modules.Resources.Objects;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Resources.Interactors;

public interface IResourcesInteractor
{
    Optional<FontResource> LoadFontResource(string filePath);
}