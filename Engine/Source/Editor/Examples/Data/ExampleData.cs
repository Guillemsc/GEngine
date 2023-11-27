using GEngine.Modules.Games.Core;

namespace GEngine.Editor.Examples.Data;

public sealed class ExampleData
{
    public string Name { get; }
    public GameRunner GameRunner { get; }
    
    public ExampleData(string name, GameRunner gameRunner)
    {
        Name = name;
        GameRunner = gameRunner;
    }
}