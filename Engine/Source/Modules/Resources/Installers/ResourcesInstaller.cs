using GEngine.Modules.Resources.Data;
using GEngine.Modules.Resources.Interactors;
using GEngine.Modules.Resources.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Resources.Installers;

public static class ResourcesInstaller
{
    public static void InstallResources(this IDiContainerBuilder builder)
    {
        builder.Bind<IResourcesInteractor>()
            .FromFunction(c => new ResourcesInteractor(
                c.Resolve<LoadFontResourceUseCase>()
            ));
        
        builder.Bind<LoadedResourcesData>().FromNew();
        
        builder.Bind<AddLoadedResourceUseCase>()
            .FromFunction(c => new AddLoadedResourceUseCase(
                c.Resolve<LoadedResourcesData>()
            ));

        builder.Bind<LoadFontResourceUseCase>()
            .FromFunction(c => new LoadFontResourceUseCase(
                c.Resolve<AddLoadedResourceUseCase>()
            ));
    }
}