using GEngine.Modules.Resources.Objects;

namespace GEngine.Modules.Resources.Data;

public sealed class LoadedResourcesData
{
    public Dictionary<Type, List<Resource>> LoadedResources { get; } = new();
}