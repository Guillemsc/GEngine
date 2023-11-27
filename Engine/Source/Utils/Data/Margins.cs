namespace GEngine.Utils.Data
{
    /// <summary>
    /// Represents some margins of a rectangle.
    /// </summary>
    public struct Margins
    {
        public float Left;
        public float Right;
        public float Top;
        public float Bottom;

        public Margins(float left, float right, float top, float bottom)
        {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public override string ToString()
        {
            return $"Left:{Left} Right:{Right} Top:{Top} Bottom:{Bottom}";
        }
    }
}
