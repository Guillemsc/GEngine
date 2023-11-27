using GEngine.Core.Interactors;
using GEngine.Utils.Di.Contexts;
using GEngine.Utils.Disposing.Disposables;
using GEngine.Core.Installers;
using GEngine.Editor.EditorWindows.Installers;
using GEngine.Editor.Examples.Installers;
using GEngine.Editor.HierarchyEditor.Installers;
using GEngine.Editor.ImGuiDemoEditor.Installers;
using GEngine.Editor.Physics2dEditor.Installers;
using GEngine.Editor.ToolbarEditor.Installers;
using GEngine.Editor.WindowSettingsEditor.Installers;
using GEngine.Modules.Cameras.Installers;
using GEngine.Modules.Tickables.Installers;
using GEngine.Modules.Windows.Installers;
using GEngine.Modules.EditorRenderer.Installers;
using GEngine.Modules.Entities.Installers;
using GEngine.Modules.Framerate.Installers;
using GEngine.Modules.Games.Installers;
using GEngine.Modules.GuizmoRenderer2d.Installers;
using GEngine.Modules.Logging.Installers;
using GEngine.Modules.Modes.Installers;
using GEngine.Modules.Physics2d.Installers;
using GEngine.Modules.Renderer2d.Installers;
using GEngine.Modules.Rendering.Installers;

namespace GEngine.Core.Bootstrap;

public static class REngineBootstrap
{
    public static IDisposable<IREngineInteractor> Execute()
    {
        DiContext<IREngineInteractor> diContext = new();

        diContext.AddInstaller(b =>
        {
            b.InstallCoreInteractor();
            b.InstallCoreUseCases();
            
            b.InstallModes();
            b.InstallLogging();
            b.InstallWindows();
            b.InstallFramerate();
            b.InstallTickables();
            b.InstallCameras();
            b.InstallRendering();
            b.InstallRenderer2d();
            b.InstallGuizmoRenderer2d();
            b.InstallEditorRenderer();
            b.InstallPhysics2d();
            b.InstallEntities();
            
            b.InstallGames();
            
            b.InstallImGuiDemoEditor();
            b.InstallToolbarEditor();
            b.InstallEditorWindows();
            b.InstallHierarchyEditor();
            b.InstallPhysics2dEditor();
            b.InstallWindowSettingsEditor();
            b.InstallExamples();
        });
        
        return diContext.Install();
    }
}