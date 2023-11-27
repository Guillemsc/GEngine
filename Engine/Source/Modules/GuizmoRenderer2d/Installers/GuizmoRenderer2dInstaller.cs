using GEngine.Modules.GuizmoRenderer2d.Data;
using GEngine.Modules.GuizmoRenderer2d.Interactors;
using GEngine.Modules.GuizmoRenderer2d.UseCases;
using GEngine.Modules.Physics2d.UseCases;
using GEngine.Utils.Di.Builder;

namespace GEngine.Modules.GuizmoRenderer2d.Installers;

public static class GuizmoRenderer2dInstaller
{
    public static void InstallGuizmoRenderer2d(this IDiContainerBuilder builder)
    {
        builder.Bind<IGuizmoRenderer2dInteractor>()
            .FromFunction(c => new GuizmoRenderer2dInteractor(
                c.Resolve<DrawLineGuizmo2dUseCase>(),
                c.Resolve<DrawQuadGuizmo2dUseCase>()
            ));
        
        builder.Bind<GuizmoRenderer2dRenderingQueueData>().FromNew();
        
        builder.Bind<RenderGuizmos2dUseCase>()
            .FromFunction(c => new RenderGuizmos2dUseCase(
                c.Resolve<DrawAllPhysicsWithGuizmosUseCase>(),
                c.Resolve<RenderGuizmos2dRenderQueueUseCase>()
            ));

        builder.Bind<RenderGuizmos2dRenderQueueUseCase>()
            .FromFunction(c => new RenderGuizmos2dRenderQueueUseCase(
                c.Resolve<GuizmoRenderer2dRenderingQueueData>()
            ));

        builder.Bind<AddToGuizmoRendering2dQueueUseCase>()
            .FromFunction(c => new AddToGuizmoRendering2dQueueUseCase(
                c.Resolve<GuizmoRenderer2dRenderingQueueData>()
            ));

        builder.Bind<DrawLineGuizmo2dUseCase>()
            .FromFunction(c => new DrawLineGuizmo2dUseCase(
                c.Resolve<AddToGuizmoRendering2dQueueUseCase>()
            ));

        builder.Bind<DrawQuadGuizmo2dUseCase>()
            .FromFunction(c => new DrawQuadGuizmo2dUseCase(
                c.Resolve<AddToGuizmoRendering2dQueueUseCase>()
            ));

        builder.Bind<DrawAllPhysicsWithGuizmosUseCase>()
            .FromFunction(c => new DrawAllPhysicsWithGuizmosUseCase(
                c.Resolve<GetAllPhysicsBodies2dUseCase>(),
                c.Resolve<GetAllPhysicsBody2dFixturesUseCase>(),
                c.Resolve<AddToGuizmoRendering2dQueueUseCase>()
            ));
    }
}