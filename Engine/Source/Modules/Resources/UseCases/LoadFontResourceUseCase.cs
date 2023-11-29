using GEngine.Modules.Resources.Objects;
using GEngine.Utils.Optionals;
using Raylib_cs;

namespace GEngine.Modules.Resources.UseCases;

public sealed class LoadFontResourceUseCase
{
    readonly AddLoadedResourceUseCase _addLoadedResourceUseCase;

    public LoadFontResourceUseCase(
        AddLoadedResourceUseCase addLoadedResourceUseCase
        )
    {
        _addLoadedResourceUseCase = addLoadedResourceUseCase;
    }

    public Optional<FontResource> Execute(string filePath)
    {
        Guid uid = Guid.NewGuid();
        
        Font font = Raylib.LoadFont(filePath);

        FontResource fontResource = new FontResource(filePath, uid, font);
        
        _addLoadedResourceUseCase.Execute(fontResource);

        return fontResource;
    }
}