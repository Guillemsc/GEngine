using GEngine.Modules.Resources.Data;
using GEngine.Modules.Resources.Objects;
using GEngine.Utils.Optionals;

namespace GEngine.Modules.Resources.UseCases;

public sealed class AddLoadedResourceUseCase
{
    readonly LoadedResourcesData _loadedResourcesData;

    public AddLoadedResourceUseCase(
        LoadedResourcesData loadedResourcesData
        )
    {
        _loadedResourcesData = loadedResourcesData;
    }

    public void Execute(Resource resource)
    {
        Type type = resource.GetType();

        bool resourcesListExists = _loadedResourcesData.LoadedResources.TryGetValue(type, out List<Resource>? resources);

        if (!resourcesListExists)
        {
            resources = new List<Resource>();
            _loadedResourcesData.LoadedResources.Add(type, resources);
        }
        
        resources!.Add(resource);
    }
}