using Raylib_cs;

namespace GEngine.Modules.Resources.Objects;

public sealed class FontResource : Resource
{
    public Font Font { get; }
    
    public FontResource(string filePath, Guid uid, Font font) : base(filePath, uid)
    {
        Font = font;
    }
}