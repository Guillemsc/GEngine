using System.Numerics;
using Raylib_cs;

namespace GEngine.Modules.GuizmoRenderer2d.Interactors;

public interface IGuizmoRenderer2dInteractor
{
    void DrawLine(Vector2 start, Vector2 end, Color color);
    void DrawQuad(Vector2 center, Vector2 size, Color color);
}