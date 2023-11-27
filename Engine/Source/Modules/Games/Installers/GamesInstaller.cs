using GEngine.Modules.Entities.UseCases;
using GEngine.Modules.Games.Data;
using GEngine.Modules.Games.Interactors;
using GEngine.Modules.Games.UseCases;
using GEngine.Modules.Tickables.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.Games.Installers;

public static class GamesInstaller
{
    public static void InstallGames(this IDiContainerBuilder builder)
    {
        builder.Bind<IGamesInteractor>()
            .FromFunction(c => new GamesInteractor(
                c.Resolve<LoadGameUseCase>(),
                c.Resolve<UnloadCurrentGameUseCase>()
            ));
        
        builder.Bind<GamesData>().FromNew();

        builder.Bind<UnloadCurrentGameUseCase>()
            .FromFunction(c => new UnloadCurrentGameUseCase(
                c.Resolve<GamesData>(),
                c.Resolve<RemoveAllTickablesUseCase>(),
                c.Resolve<DestroyAllActiveEntitiesUseCase>()
            ));

        builder.Bind<LoadGameUseCase>()
            .FromFunction(c => new LoadGameUseCase(
                c.Resolve<GamesData>(),
                c.Resolve<UnloadCurrentGameUseCase>(),
                c.Resolve<AddTickableUseCase>()
            ));
    }
}