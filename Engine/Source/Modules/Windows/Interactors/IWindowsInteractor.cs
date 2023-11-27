using System.Numerics;

namespace GEngine.Modules.Windows.Interactors;

public interface IWindowsInteractor
{
    void ShowWindow(int width, int height, string title);
    bool IsCloseWindowRequested();
    Vector2 GetScreenSize();
}