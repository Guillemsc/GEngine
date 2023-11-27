using System.Numerics;

namespace GEngine.Utils.Extensions
{
    public static class TrigonometryExtensions
    {
        /// <summary>
        /// Calculates the height of an equilateral triangle given the length of one of its sides.
        /// </summary>
        /// <param name="sideDistance">The length of one of the sides of the equilateral triangle.</param>
        /// <returns>The height of the equilateral triangle.</returns>
        public static float GetEquilateralTriangleHeight(float sideDistance)
        {
            // The height of an equilateral triangle with side length s is (s*sqrt(3))/2
            return (sideDistance * (float)Math.Sqrt(3f)) / 2f;
        }


        /// <summary>
        /// Calculates a vector that is perpendicular to both <paramref name="lhs"/> and <paramref name="rhs"/>.
        /// Use the left hand side rule to see how the resulting vector looks
        /// https://www.youtube.com/watch?v=kz92vvioeng
        /// </summary>
        /// <param name="lhs">The first vector.</param>
        /// <param name="rhs">The second vector.</param>
        /// <returns>A vector that is perpendicular to both <paramref name="lhs"/> and <paramref name="rhs"/>.</returns>
        public static Vector3 GetPerpendicular(Vector3 lhs, Vector3 rhs)
        {
            return Vector3.Cross(lhs, rhs);
        }
    }
}
