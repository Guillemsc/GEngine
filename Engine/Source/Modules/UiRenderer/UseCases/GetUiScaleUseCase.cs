using System.Numerics;
using GEngine.Modules.UiRenderer.Data;
using GEngine.Modules.Windows.UseCases;
using GEngine.Utils.Extensions;

namespace GEngine.Modules.UiRenderer.UseCases;

public sealed class GetUiScaleUseCase
{
    readonly UiReferenceResolutionData _uiReferenceResolutionData;
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;

    public GetUiScaleUseCase(
        UiReferenceResolutionData uiReferenceResolutionData, 
        GetWindowSizeUseCase getWindowSizeUseCase
        )
    {
        _uiReferenceResolutionData = uiReferenceResolutionData;
        _getWindowSizeUseCase = getWindowSizeUseCase;
    }

    public float Execute()
    {
        Vector2 windowSize = _getWindowSizeUseCase.Execute();

        float horizontalAspect = MathExtensions.Divide(windowSize.X, _uiReferenceResolutionData.ReferenceResolution.X);
        float verticalAspect = MathExtensions.Divide(windowSize.Y, _uiReferenceResolutionData.ReferenceResolution.Y);

        float horizontalScaleFactor = _uiReferenceResolutionData.ScaleHorizontallyOrVerticallyFactor;
        float verticalScaleFactor = 1 - _uiReferenceResolutionData.ScaleHorizontallyOrVerticallyFactor;
        
        float finalScale = (horizontalAspect * horizontalScaleFactor) + (verticalAspect * verticalScaleFactor);

        return finalScale;
    }
}