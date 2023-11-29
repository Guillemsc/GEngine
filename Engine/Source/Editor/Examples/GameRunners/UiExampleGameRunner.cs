using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Objects;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Games.Core;
using GEngine.Modules.Resources.Objects;
using GEngine.Utils.Optionals;
using Raylib_cs;

namespace GEngine.Editor.Examples.GameRunners;

public sealed class UiExampleGameRunner : GameRunner
{
    UiEntity _ui1;
    UiEntity _ui2;
    UiEntity _ui3;
    UiEntity _text;
    
    public UiExampleGameRunner(IREngineInteractor engine) : base(engine)
    {
    }

    public override void Start()
    {
        _ui1 = Engine.Entities.CreateUi("Ui");
        _ui1.RectUi.SetSizeDelta(new Vector2(0, 100));
        _ui1.RectUi.SetAnchorExpandBottom();
        _ui1.RectUi.SetPivotBottomCenter();
        _ui1.RectUi.SetAnchoredPosition(new Vector2(0, 0));
        BoxRendererUiComponent br1 = _ui1.AddComponent<BoxRendererUiComponent>();
        
        _ui2 = Engine.Entities.CreateUi("Ui2");
        _ui2.RectUi.SetSizeDelta(new Vector2(-10, -10));
        _ui2.RectUi.SetAnchorExpand();
        BoxRendererUiComponent _br2 = _ui2.AddComponent<BoxRendererUiComponent>();
        _br2.SetColor(Color.BLUE);
        _br2.SetSortingOrder(1);
        
        _ui3 = Engine.Entities.CreateUi("Ui3");
        _ui3.RectUi.SetSizeDelta(new Vector2(60, 50));
        _ui3.RectUi.SetAnchorTopLeft();
        _ui3.RectUi.SetAnchoredPosition(new Vector2(30, -25));
        BoxRendererUiComponent _br3 = _ui3.AddComponent<BoxRendererUiComponent>();
        _br3.SetColor(Color.GOLD);
        _br3.SetSortingOrder(2);
        
        _ui1.AddChild(_ui2);
        _ui2.AddChild(_ui3);

        Optional<FontResource> font = Engine.Resources.LoadFontResource("resources/ARIAL.TTF");
        
        _text = Engine.Entities.CreateUi("Text");
        BoxRendererUiComponent _br4 = _text.AddComponent<BoxRendererUiComponent>();
        TextRendererUiComponent tr3 = _text.AddComponent<TextRendererUiComponent>();
        tr3.SetSortingOrder(3);
        tr3.SetFont(font.UnsafeGet());
        tr3.SetText("Lorem ipsum armin sasageyo Lorem ipsum armin sasageyo Lorem ipsum armin sasageyo");
        
        _text.RectUi.SetSizeDelta(new Vector2(300, 300));
    }

    public override void Tick()
    {
        Vector2 size = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) size.Y += 2;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) size.Y -= 2;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) size.X += 2;
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) size.X -= 2;
        _text.RectUi.AddSizeDelta(size);
    }

    public override void Dispose()
    {
        
    }
}