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
                c.Resolve<CreateWorldEntityUseCase>(),
                c.Resolve<CreateWorldEntityWithParentUseCase>(),
                c.Resolve<CreateUiEntityUseCase>(),
                c.Resolve<DestroyAllActiveEntitiesUseCase>()
            ));

        builder.Bind<SceneData>().FromNew();
        
        builder.Bind<CreateWorldEntityUseCase>()
            .FromFunction(c => new CreateWorldEntityUseCase(
                c.LazyResolve<IREngineInteractor>(),
                c.Resolve<RefreshEntityOnSceneUseCase>(),
                c.Resolve<RefreshEntityOnActiveEntitiesListsUseCase>()
            ));

        builder.Bind<CreateWorldEntityWithParentUseCase>()
            .FromFunction(c => new CreateWorldEntityWithParentUseCase(
                c.Resolve<CreateWorldEntityUseCase>()
            ));

        builder.Bind<CreateUiEntityUseCase>()
            .FromFunction(c => new CreateUiEntityUseCase(
                c.LazyResolve<IREngineInteractor>(),
                c.Resolve<RefreshEntityOnSceneUseCase>(),
                c.Resolve<RefreshEntityOnActiveEntitiesListsUseCase>()
            ));

        builder.Bind<DestroyEntityUseCase>()
            .FromFunction(c => new DestroyEntityUseCase());

        builder.Bind<DestroyAllActiveEntitiesUseCase>()
            .FromFunction(c => new DestroyAllActiveEntitiesUseCase(
                c.Resolve<SceneData>(),
                c.Resolve<DestroyEntityUseCase>()
            ));

        builder.Bind<RefreshEntityOnSceneUseCase>()
            .FromFunction(c => new RefreshEntityOnSceneUseCase(
                c.Resolve<RefreshEntityOnActiveEntitiesListsUseCase>(),
                c.Resolve<GetSceneDataForEntityTypeUseCase>()
            ));
        
        builder.Bind<RefreshEntityOnActiveEntitiesListsUseCase>()
            .FromFunction(c => new RefreshEntityOnActiveEntitiesListsUseCase(
                c.Resolve<GetSceneDataForEntityTypeUseCase>()
            ));

        builder.Bind<GetRootSceneActiveEntitiesUseCase>()
            .FromFunction(c => new GetRootSceneActiveEntitiesUseCase(
                c.Resolve<SceneData>()
            ));

        builder.Bind<TickEntitiesUseCase>()
            .FromFunction(c => new TickEntitiesUseCase(
                c.Resolve<TickActiveEntitiesUseCase>()
            ));

        builder.Bind<TickActiveEntitiesUseCase>()
            .FromFunction(c => new TickActiveEntitiesUseCase(
                c.Resolve<SceneData>()
            ));

        builder.Bind<GetSceneDataForEntityTypeUseCase>()
            .FromFunction(c => new GetSceneDataForEntityTypeUseCase(
                c.Resolve<SceneData>()
            ));
    }
}