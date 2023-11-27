using GEngine.Core.Bootstrap;
using GEngine.Core.Interactors;
using GEngine.Editor.Examples.GameRunners;
using GEngine.Modules.Modes.Enums;
using GEngine.Utils.Disposing.Disposables;

namespace Game
{
    static class Program
    {
        public static void Main()
        {
            IDisposable<IREngineInteractor> disposableEngine = REngineBootstrap.Execute();
            IREngineInteractor engine = disposableEngine.Value;
            
            engine.Modes.SetEngineMode(EngineModeType.Debug);

            Pong2dExampleGameRunner transformPositionExampleGameRunner = new Pong2dExampleGameRunner(engine);
            engine.Games.Load(transformPositionExampleGameRunner);

            while (!engine.Windows.IsCloseWindowRequested())
            {
                engine.Tickables.Tick();
            }

            disposableEngine.Dispose();
        }
    }
}