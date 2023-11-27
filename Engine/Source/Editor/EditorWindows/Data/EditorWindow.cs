namespace GEngine.Editor.EditorWindows.Data;

public sealed class EditorWindow
{
    public string Name { get; }
    public Action<bool> SetActive { get; }
    public Func<bool> GetActive { get; }

    public EditorWindow(
        string name, 
        Action<bool> setActive,
        Func<bool> getActive
        )
    {
        Name = name;
        SetActive = setActive;
        GetActive = getActive;
    }
}