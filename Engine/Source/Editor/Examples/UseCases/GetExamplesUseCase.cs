using GEngine.Editor.Examples.Data;

namespace GEngine.Editor.Examples.UseCases;

public sealed class GetExamplesUseCase
{
    readonly ExamplesData _examplesData;

    public GetExamplesUseCase(ExamplesData examplesData)
    {
        _examplesData = examplesData;
    }

    public IReadOnlyList<ExampleData> Execute()
    {
        return _examplesData.Examples;
    }
}