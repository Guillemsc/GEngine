using System.Numerics;
using GEngine.Editor.WindowSettingsEditor.Data;
using GEngine.Modules.Windows.UseCases;
using ImGuiNET;

namespace GEngine.Editor.WindowSettingsEditor.UseCases;

public sealed class RenderWindowSettingsEditorUseCase
{
    readonly WindowSettingsEditorData _windowSettingsEditorData;
    readonly GetWindowSizeUseCase _getWindowSizeUseCase;
    readonly GetMSAAEnabledUseCase _getMsaaEnabledUseCase;
    readonly SetVSyncEnabledUseCase _setVSyncEnabledUseCase;
    readonly GetVSyncEnabledUseCase _getVSyncEnabledUseCase;
    readonly SetWindowFullscreenEnabledUseCase _setWindowFullscreenEnabledUseCase;
    readonly GetWindowFullscreenEnabledUseCase _getWindowFullscreenEnabledUseCase;
    readonly SetWindowResizableEnabledUseCase _setWindowResizableEnabledUseCase;
    readonly GetWindowResizableEnabledUseCase _getWindowResizableEnabledUseCase;

    public RenderWindowSettingsEditorUseCase(
        WindowSettingsEditorData windowSettingsEditorData, 
        GetWindowSizeUseCase getWindowSizeUseCase, 
        GetMSAAEnabledUseCase getMsaaEnabledUseCase,
        SetVSyncEnabledUseCase setVSyncEnabledUseCase, 
        GetVSyncEnabledUseCase getVSyncEnabledUseCase, 
        SetWindowFullscreenEnabledUseCase setWindowFullscreenEnabledUseCase, 
        GetWindowFullscreenEnabledUseCase getWindowFullscreenEnabledUseCase, 
        SetWindowResizableEnabledUseCase setWindowResizableEnabledUseCase, 
        GetWindowResizableEnabledUseCase getWindowResizableEnabledUseCase
        )
    {
        _windowSettingsEditorData = windowSettingsEditorData;
        _getWindowSizeUseCase = getWindowSizeUseCase;
        _getMsaaEnabledUseCase = getMsaaEnabledUseCase;
        _setVSyncEnabledUseCase = setVSyncEnabledUseCase;
        _getVSyncEnabledUseCase = getVSyncEnabledUseCase;
        _setWindowFullscreenEnabledUseCase = setWindowFullscreenEnabledUseCase;
        _getWindowFullscreenEnabledUseCase = getWindowFullscreenEnabledUseCase;
        _setWindowResizableEnabledUseCase = setWindowResizableEnabledUseCase;
        _getWindowResizableEnabledUseCase = getWindowResizableEnabledUseCase;
    }

    public void Execute()
    {
        if (!_windowSettingsEditorData.Active)
        {
            return;
        }
        
        if (ImGui.Begin("Window Settings", ref _windowSettingsEditorData.Active))
        {
            Vector2 windowSize = _getWindowSizeUseCase.Execute();
            bool msaaEnabled = _getMsaaEnabledUseCase.Execute();
            
            ImGui.Text($"Size {windowSize}");
            ImGui.Text($"MSAA {msaaEnabled}");

            DrawCheckbox("VSync", _getVSyncEnabledUseCase.Execute, _setVSyncEnabledUseCase.Execute);
            DrawCheckbox("Fullscreen", _getWindowFullscreenEnabledUseCase.Execute, _setWindowFullscreenEnabledUseCase.Execute);
            DrawCheckbox("Resizable", _getWindowResizableEnabledUseCase.Execute, _setWindowResizableEnabledUseCase.Execute);

            ImGui.End();
        }
    }

    void DrawCheckbox(string name, Func<bool> _get, Action<bool> set)
    {
        bool value = _get.Invoke();
        bool valueChanged = ImGui.Checkbox(name, ref value);

        if (valueChanged)
        {
            set.Invoke(value);
        }
    }
}