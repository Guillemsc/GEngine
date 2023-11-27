using System.Numerics;
using Raylib_cs;

namespace GEngine.Modules.Windows.UseCases;

public sealed class GetWindowSizeUseCase
{
    public Vector2 Execute()
    {
        float width = Raylib.GetScreenWidth();       
        float height = Raylib.GetScreenHeight();

        return new Vector2(width, height);
    }
}