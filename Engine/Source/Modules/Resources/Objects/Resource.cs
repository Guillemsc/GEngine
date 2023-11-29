namespace GEngine.Modules.Resources.Objects;

public abstract class Resource
{
    public string FilePath { get; }
    public Guid Uid { get; }
    
    protected Resource(string filePath, Guid uid)
    {
        FilePath = filePath;
        Uid = uid;
    }
}