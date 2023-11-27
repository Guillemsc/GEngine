using GEngine.Core.Interactors;
using GEngine.Editor.Examples.Data;
using GEngine.Editor.Examples.GameRunners;
using GEngine.Editor.Examples.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Editor.Examples.Installers;

public static class ExamplesInstallers
{
    public static void InstallExamples(this IDiContainerBuilder builder)
    {
        builder.Bind<ExamplesData>()
            .FromNew()
            .WhenInit((c, o) =>
            {
                o.Examples.Add(new ExampleData(
                    "Transform Position",
                    new TransformPositionExampleGameRunner(c.Resolve<IREngineInteractor>())
                ));
                
                o.Examples.Add(new ExampleData(
                    "Transform Scale",
                    new TransformScaleExampleGameRunner(c.Resolve<IREngineInteractor>())
                ));
                
                o.Examples.Add(new ExampleData(
                    "Physics",
                    new PhysicsExampleGameRunner(c.Resolve<IREngineInteractor>())
                ));
                
                o.Examples.Add(new ExampleData(
                    "Camera2d",
                    new Camera2dExampleGameRunner(c.Resolve<IREngineInteractor>())
                    ));

                o.Examples.Add(new ExampleData(
                    "Pong2d",
                    new Pong2dExampleGameRunner(c.Resolve<IREngineInteractor>())
                ));
            });

        builder.Bind<GetExamplesUseCase>()
            .FromFunction(c => new GetExamplesUseCase(
                c.Resolve<ExamplesData>()
            ));
    }
}