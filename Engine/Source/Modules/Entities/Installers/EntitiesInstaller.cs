using GEngine.Core.Interactors;
using GEngine.Modules.Entities.Data;
using GEngine.Modules.Entities.Interactors;
using GEngine.Modules.Entities.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Entities.Installers;

public static class EntitiesInstaller
{
    public static void InstallEntities(this IDiContainerBuilder builder)
    {
        builder.Bind<IEntitiesInteractor>()
            .FromFunction(c => new EntitiesInteractor(
                c.Resolve<CreateEntityUseCase>(),
                c.Resolve<CreateEntityWithParentUseCase>(),
                c.Resolve<DestroyAllActiveEntitiesUseCase>()
            ));

        builder.Bind<SceneEntitiesData>().FromNew();
        
        builder.Bind<CreateEntityUseCase>()
            .FromFunction(c => new CreateEntityUseCase(
                c.LazyResolve<IREngineInteractor>(),
                c.Resolve<RefreshEntityOnSceneUseCase>(),
                c.Resolve<RefreshEntityOnActiveEntitiesListsUseCase>()
            ));

        builder.Bind<CreateEntityWithParentUseCase>()
            .FromFunction(c => new CreateEntityWithParentUseCase(
                c.Resolve<CreateEntityUseCase>()
            ));

        builder.Bind<DestroyEntityUseCase>()
            .FromFunction(c => new DestroyEntityUseCase());

        builder.Bind<DestroyAllActiveEntitiesUseCase>()
            .FromFunction(c => new DestroyAllActiveEntitiesUseCase(
                c.Resolve<SceneEntitiesData>(),
                c.Resolve<DestroyEntityUseCase>()
            ));

        builder.Bind<RefreshEntityOnSceneUseCase>()
            .FromFunction(c => new RefreshEntityOnSceneUseCase(
                c.Resolve<SceneEntitiesData>(),
                c.Resolve<RefreshEntityOnActiveEntitiesListsUseCase>()
            ));
        
        builder.Bind<RefreshEntityOnActiveEntitiesListsUseCase>()
            .FromFunction(c => new RefreshEntityOnActiveEntitiesListsUseCase(
                c.Resolve<SceneEntitiesData>()
            ));

        builder.Bind<GetRootActiveEntitiesUseCase>()
            .FromFunction(c => new GetRootActiveEntitiesUseCase(
                c.Resolve<SceneEntitiesData>()
            ));

        builder.Bind<TickEntitiesUseCase>()
            .FromFunction(c => new TickEntitiesUseCase(
                c.Resolve<TickActiveEntitiesUseCase>()
            ));

        builder.Bind<TickActiveEntitiesUseCase>()
            .FromFunction(c => new TickActiveEntitiesUseCase(
                c.Resolve<SceneEntitiesData>()
            ));
    }
}