using System.Numerics;
using GEngine.Core.Interactors;
using GEngine.Modules.Components.Interfaces;
using GEngine.Modules.Entities.Objects;
using GEngine.Modules.Resources.Objects;
using GEngine.Utils.Extensions;
using GEngine.Utils.Optionals;
using Raylib_cs;

namespace GEngine.Modules.Components.Objects;

public sealed class TextRendererUiComponent : UiComponent, INotifyRectUiChanged
{
    public Optional<FontResource> Font { get; private set; }
    
    public int SortingOrder { get; private set; }
    public float Size { get; private set; } = 32f;
    public float Spacing { get; private set; } = 0f;
    public string Text { get; private set; } = "Text";

    bool _wordWrapping = true;
    
    List<string> _lines = new();
    float _lineWidth;
    
    public TextRendererUiComponent(IREngineInteractor engine, Guid uid, BaseEntity<UiComponent> owner) : base(engine, uid, owner)
    {
        
    }

    public override void Tick()
    {
        bool hasFont = Font.TryGet(out FontResource fontResource);

        if (!hasFont)
        {
            return;
        }

        RegenerateLayout();
        
        void Render(float scale)
        {
            Rectangle uiRectangle = Owner.RectUi.Rectangle;
            
            for (int i = 0; i < _lines.Count; i++)
            {
                string line = _lines[i];
            
                Vector2 position = new Vector2(
                    uiRectangle.X - (uiRectangle.Width * 0.5f),
                    -uiRectangle.Y - (uiRectangle.Height * 0.5f) + (i * 40)
                );
                
                Raylib.DrawTextEx(
                    fontResource.Font,
                    line,
                    position,
                    Size,
                    Spacing, 
                    Color.BLACK
                );
            }
        }
        
        Engine.UiRenderer.AddToUiRenderingQueue(SortingOrder, Render);
    }

    public void SetFont(FontResource fontResource)
    {
        Font = fontResource;
    }
    
    public void SetSortingOrder(int sortingOrder)
    {
        SortingOrder = sortingOrder;
    }

    public void SetText(string text)
    {
        Text = text;
    }

    void RegenerateLayout()
    {
        _lines.Clear();
        
        bool hasFont = Font.TryGet(out FontResource fontResource);

        if (!hasFont)
        {
            return;
        }
        
        Rectangle uiRectangle = Owner.RectUi.Rectangle;
        
        float scaleFactor = Size / fontResource.Font.BaseSize;

        string currentLine = string.Empty;
        float currentLineWidth = 0f;

        string currentWord = string.Empty;
        float currentWordWidth = 0f;
        
        for (int i = 0; i < Text.Length; i++)
        {
            char character = Text[i];

            bool isSpaceCharacter = (character == ' ') || (character == '\t') || (character == '\n');
            
            float glyphWidth = 0;
            
            if (character != '\n')
            {
                GlyphInfo glyph = Raylib.GetGlyphInfo(fontResource.Font, character);
                Rectangle glyphRectangle = Raylib.GetGlyphAtlasRec(fontResource.Font, character);

                glyphWidth = glyph.AdvanceX == 0
                    ? glyphRectangle.Width * scaleFactor
                    : glyph.AdvanceX * scaleFactor;

                if (i + 1 < Text.Length)
                {
                    glyphWidth += Spacing;
                }
            }

            if (!_wordWrapping)
            {
                bool isSpaceAtStartOfLine = currentLine.Length == 0 && isSpaceCharacter;
                
                if (isSpaceAtStartOfLine)
                {
                    continue;
                }
                
                float nextCurrentLineWidth = currentLineWidth + glyphWidth;

                if (nextCurrentLineWidth > uiRectangle.Width)
                {
                    _lines.Add(currentLine);
                    currentLine = string.Empty;
                    currentLineWidth = 0f;
                }
                else
                {
                    currentLine += character;
                    currentLineWidth = nextCurrentLineWidth;
                }   
            }
            else
            {
                float nextCurrentLineWidth = currentLineWidth + currentWordWidth;
                
                if (isSpaceCharacter)
                {
                    if (nextCurrentLineWidth > uiRectangle.Width)
                    {
                        _lines.Add(currentLine);
                        
                        currentLine = currentWord + character;
                        currentLineWidth = currentWordWidth + glyphWidth;
                    }
                    else
                    {
                        currentLine += currentWord + character;
                        currentLineWidth = nextCurrentLineWidth + glyphWidth;
                    }
                    
                    currentWord = string.Empty;
                    currentWordWidth = 0f;
                }
                else
                {
                    currentWord += character;
                    currentWordWidth += glyphWidth;
                }
            }
        }

        if (!_wordWrapping)
        {
            _lines.Add(currentLine);
        }
        else
        {
            float nextCurrentLineWidth = currentLineWidth + currentWordWidth;
                
            if (nextCurrentLineWidth > uiRectangle.Width)
            {
                _lines.Add(currentLine);
                _lines.Add(currentWord);
            }
            else
            {
                currentLine += currentWord;
                _lines.Add(currentLine);
            }
        }
    }

    public void OnRectUiChanged()
    {
        RegenerateLayout();
    }

    enum State
    {
        MEASURE_STATE,
        DRAW_STATE,
    }
    
    // Draw text using font inside rectangle limits with support for text selection
    static void DrawTextBoxedSelectable(
        Font font,
        string text,
        Rectangle rec,
        float fontSize,
        float spacing,
        bool wordWrap
    )
    {
        int length = text.Length; // Total length in bytes of the text, scanned by codepoints in loop
    
        float textOffsetY = 0; // Offset between lines (on line break '\n')
        float textOffsetX = 0.0f; // Offset X to next character to draw
    
        float scaleFactor = fontSize / font.BaseSize; // Character rectangle scaling factor
    
        // Word/character wrapping mechanism variables
        State state = wordWrap ? State.MEASURE_STATE : State.DRAW_STATE;
    
        int startLine = -1; // Index where to begin drawing (where a line begins)
        int endLine = -1; // Index where to stop drawing (where a line ends)
        int lastk = -1; // Holds last value of the character position

        int k = 0;
        for (int i = 0; i < length; i++)
        {
            char character = text[i];
            
            // Get next codepoint from byte string and glyph index in font
            int codepointByteCount = 0;
            int index = Raylib.GetGlyphIndex(font, character);
    
            // NOTE: Normally we exit the decoding sequence as soon as a bad byte is found (and return 0x3f)
            // but we need to draw all of the bad bytes using the '?' symbol moving one byte
            if (character == 0x3f)
            {
                codepointByteCount = 1;
            }
            
            i += (codepointByteCount - 1);
    
            float glyphWidth = 0;
            if (character != '\n')
            {
                GlyphInfo glyph = Raylib.GetGlyphInfo(font, index);
                Rectangle glyphRectangle = Raylib.GetGlyphAtlasRec(font, index);

                glyphWidth = (glyph.AdvanceX == 0)
                    ? glyphRectangle.Width * scaleFactor
                    : glyph.AdvanceX * scaleFactor;

                if (i + 1 < length)
                {
                    glyphWidth += spacing;
                }
            }
    
            // NOTE: When wordWrap is ON we first measure how much of the text we can draw before going outside of the rec container
            // We store this info in startLine and endLine, then we change states, draw the text between those two variables
            // and change states again and again recursively until the end of the text (or until we get outside of the container).
            // When wordWrap is OFF we don't need the measure state so we go to the drawing state immediately
            // and begin drawing on the next line before we can get outside the container.
            if (state == State.MEASURE_STATE)
            {
                // TODO: There are multiple types of spaces in UNICODE, maybe it's a good idea to add support for more
                // Ref: http://jkorpela.fi/chars/spaces.html
                if ((character == ' ') || (character == '\t') || (character == '\n'))
                {
                    endLine = i;
                }
    
                if ((textOffsetX + glyphWidth) > rec.Width)
                {
                    endLine = (endLine < 1) ? i : endLine;
                    if (i == endLine) endLine -= codepointByteCount;
                    if ((startLine + codepointByteCount) == endLine) endLine = (i - codepointByteCount);
    
                    state = State.DRAW_STATE;
                }
                else if ((i + 1) == length)
                {
                    endLine = i;
                    state = State.DRAW_STATE;
                }
                else if (character == '\n')
                {
                    state = State.DRAW_STATE;
                }
    
                if (state == State.DRAW_STATE)
                {
                    textOffsetX = 0;
                    i = startLine;
                    glyphWidth = 0;
    
                    // Save character position when we switch states
                    int tmp = lastk;
                    lastk = k - 1;
                    k = tmp;
                }
            }
            else
            {
                if (character == '\n')
                {
                    if (!wordWrap)
                    {
                        textOffsetY += (font.BaseSize + font.BaseSize / 2) * scaleFactor;
                        textOffsetX = 0;
                    }
                }
                else
                {
                    if (!wordWrap && ((textOffsetX + glyphWidth) > rec.Width))
                    {
                        textOffsetY += (font.BaseSize + font.BaseSize / 2) * scaleFactor;
                        textOffsetX = 0;
                    }
    
                    // When text overflows rectangle height limit, just stop drawing
                    if ((textOffsetY + font.BaseSize * scaleFactor) > rec.Height) break;
                    
                    // Draw current character glyph
                    if ((character != ' ') && (character != '\t'))
                    {
                        Vector2 position = new(
                            rec.X + textOffsetX,
                            rec.Y + textOffsetY
                        );
                        
                        Raylib.DrawTextCodepoint(font, character, position, fontSize, Color.BLUE);
                    }
                }
    
                if (wordWrap && (i == endLine))
                {
                    textOffsetY += (font.BaseSize + font.BaseSize / 2) * scaleFactor;
                    textOffsetX = 0;
                    startLine = endLine;
                    endLine = -1;
                    glyphWidth = 0;
                    k = lastk;
    
                    state = State.MEASURE_STATE;
                }
            }
    
            if ((textOffsetX != 0) || (character != ' ')) textOffsetX += glyphWidth; // avoid leading spaces

            k++;
        }
    }
}