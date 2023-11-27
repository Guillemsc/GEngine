using System.Numerics;

namespace GEngine.Utils.Extensions
{
    public static class Vector2Extensions
    {
        /// <summary>
        /// Equivalent to x:<see cref="float.MaxValue"/> y:<see cref="float.MaxValue"/>.
        /// </summary>
        public static readonly Vector2 MaxValue = new(float.MaxValue, float.MaxValue);

        /// <summary>
        /// Equivalent to x:<see cref="float.MinValue"/> y:<see cref="float.MinValue"/>.
        /// </summary>
        public static readonly Vector2 MinValue = new(float.MinValue, float.MinValue);

        /// <summary>
        /// Equivalent to x:0.5 y:0.5.
        /// </summary>
        public static readonly Vector2 HalfOne = new(0.5f, 0.5f);
        

        public static Vector2 MinComponents(this IEnumerable<Vector2> vectors)
        {
            Vector2 minVector = vectors.Aggregate((currentMin, nextVector) =>
                new Vector2(Math.Min(currentMin.X, nextVector.X), Math.Min(currentMin.Y, nextVector.Y)));

            return minVector;
        }

        public static Vector2 MaxComponents(this IEnumerable<Vector2> vectors)
        {
            Vector2 maxVector = vectors.Aggregate((currentMin, nextVector) =>
                new Vector2(Math.Max(currentMin.X, nextVector.X), Math.Max(currentMin.Y, nextVector.Y)));

            return maxVector;
        }

        /// <summary>
        /// Deconstructs the Vector2 object into its individual x and y components.
        /// </summary>
        /// <param name="vector">The Vector2 object to deconstruct.</param>
        /// <param name="x">The output variable that will hold the x component of the Vector2.</param>
        /// <param name="y">The output variable that will hold the y component of the Vector2.</param>
        public static void Deconstruct(this Vector2 vector, out float x, out float y)
        {
            x = vector.X;
            y = vector.Y;
        }

        /// <summary>
        /// Returns a new Vector2 object with the specified x component, while keeping the original y component.
        /// </summary>
        /// <param name="vector">The original Vector2 object.</param>
        /// <param name="x">The new x component.</param>
        /// <returns>A new Vector2 object with the specified x component and the original y component.</returns>
        public static Vector2 WithX(this Vector2 vector, float x) => new(x, vector.Y);

        /// <summary>
        /// Returns a new Vector2 object with the specified y component, while keeping the original x component.
        /// </summary>
        /// <param name="vector">The original Vector2 object.</param>
        /// <param name="y">The new y component.</param>
        /// <returns>A new Vector2 object with the specified y component and the original x component.</returns>
        public static Vector2 WithY(this Vector2 vector, float y) => new(vector.X, y);

        /// <summary>
        /// Returns the direction Vector2 from the specified angle in degrees.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The direction Vector2 corresponding to the specified angle in degrees.</returns>
        /// <example>.[0 deegres = (1, 0)] [90 deegres = (0, 1)] [180 deegres = (-1, 0)] [270 deegres = (0, -1)]</example>
        // public static Vector2 DirectionFromAngleDegrees(float degrees)
        // {
        //     float radians = degrees * Mathf.Deg2Rad;
        //
        //     return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        // }

        /// <summary>
        /// Returns the angle in radians from the Vector2 direction.
        /// </summary>
        /// <param name="direction">The Vector2 direction.</param>
        /// <returns>The angle in radians from the Vector2 direction.</returns>
        // public static float AngleRadiansFromDirection(this Vector2 direction)
        // {
        //     return Mathf.Atan2(direction.y, direction.x);
        // }

        /// <summary>
        /// Returns the angle in degrees from the Vector2 direction.
        /// </summary>
        /// <param name="direction">The Vector2 direction.</param>
        /// <returns>The angle in degrees from the Vector2 direction.</returns>
        // public static float AngleDegreesFromDirection(this Vector2 direction)
        // {
        //     float radians = AngleRadiansFromDirection(direction);
        //
        //     return radians * Mathf.Rad2Deg;
        // }

        /// <summary>
        /// Returns the angle in radians between Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>The angle in radians between Vector2 a and Vector2 b.</returns>
        public static float AngleRadiansBetween(this Vector2 a, Vector2 b)
        {
            return (float)Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        /// <summary>
        /// Returns the angle in degrees between Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>The angle in degrees between Vector2 a and Vector2 b.</returns>
        // public static float AngleDegreesBetween(this Vector2 a, Vector2 b)
        // {
        //     float radians = a.AngleRadiansBetween(b);
        //
        //     return radians * Mathf.Rad2Deg;
        // }

        /// <summary>
        /// Rotates the Vector2 by the specified degrees.
        /// </summary>
        /// <param name="vector">The original Vector2 object.</param>
        /// <param name="degrees">The rotation angle in degrees.</param>
        /// <returns>The rotated Vector2.</returns>
        // public static Vector2 RotateDegrees(this Vector2 vector, float degrees)
        // {
        //     float sin = Math.Sin(degrees * Mathf.Deg2Rad);
        //     float cos = Math.Cos(degrees * Mathf.Deg2Rad);
        //
        //     vector.x = (cos * vector.x) - (sin * vector.y);
        //     vector.y = (sin * vector.x) + (cos * vector.y);
        //
        //     return vector;
        // }

        /// <summary>
        /// Returns a Vector2 that is a spherical linear interpolation between Vector2 a and Vector2 b by the specified factor.
        /// </summary>
        /// <param name="a">The start Vector2.</param>
        /// <param name="b">The end Vector2.</param>
        /// <param name="factor">The interpolation factor. Value is clamped between 0 and 1.</param>
        /// <returns>The interpolated Vector2.</returns>
        // public static Vector2 Slerp(Vector2 a, Vector2 b, float factor)
        // {
        //     float angle = Vector2.Angle(a, b);
        //     float angleFactor = angle * factor;
        //     float magnitude = Mathf.Lerp(a.magnitude, b.magnitude, factor);
        //
        //     Vector2 vector = a.RotateDegrees(angleFactor).normalized;
        //     Vector2 vectorWithMagnitude = vector * magnitude;
        //
        //     return vectorWithMagnitude;
        // }

        /// <summary>
        /// Returns a new Vector2 with each component (x, y) set to the maximum value
        /// between the corresponding components of Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>A new Vector2 with the maximum elements.</returns>
        public static Vector2 MaxElements(Vector2 a, Vector2 b)
            => new(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y));

        /// <summary>
        /// Returns a new Vector2 with each component (x, y) set to the minimum value
        /// between the corresponding components of Vector2 a and Vector2 b.
        /// </summary>
        /// <param name="a">The first Vector2.</param>
        /// <param name="b">The second Vector2.</param>
        /// <returns>A new Vector2 with the minimum elements.</returns>
        public static Vector2 MinElements(Vector2 a, Vector2 b)
            => new(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y));

        /// <summary>
        /// Returns the maximum value between x and y.
        /// </summary>
        /// <param name="value">The Vector2.</param>
        /// <returns>The maximum component value.</returns>
        public static float MaxComponent(this Vector2 value)
            => Math.Max(value.X, value.Y);

        /// <summary>
        /// Returns the minimum value between x and y.
        /// </summary>
        /// <param name="value">The Vector2.</param>
        /// <returns>The minimum component value.</returns>
        public static float MinComponent(this Vector2 value)
            => Math.Min(value.X, value.Y);

        /// <summary>
        /// Returns a new Vector2 with each component (x, y) set to the
        /// absolute value of the corresponding.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with absolute values.</returns>
        public static Vector2 Abs(this Vector2 vector)
            => new(vector.X.Abs(), vector.Y.Abs());

        /// <summary>
        /// Returns a new Vector2 with the x and y components swapped.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with swapped axis.</returns>
        public static Vector2 SwapAxis(this Vector2 vector)
            => new (vector.Y, vector.X);

        /// <summary>
        /// Returns a new Vector2 with the x component flipped.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with flipped x component.</returns>
        public static Vector2 FlipX(this Vector2 vector)
            => new(-vector.X, vector.Y);

        /// <summary>
        /// Returns a new Vector2 with the y component flipped.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with flipped y component.</returns>
        public static Vector2 FlipY(this Vector2 vector)
            => new(vector.X, -vector.Y);

        /// <summary>
        /// Returns a new Vector2 that is rotated 90 degrees clockwise relative to the input Vector2.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 rotated 90 degrees clockwise.</returns>
        public static Vector2 PerpendicularClockwise(this Vector2 vector)
            => new(vector.Y, -vector.X);

        /// <summary>
        /// Returns a new Vector2 that is rotated 90 degrees counter-clockwise relative to the input Vector2.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 rotated 90 degrees counter-clockwise.</returns>
        public static Vector2 PerpendicularCounterClockwise(this Vector2 vector)
            => new(-vector.Y, vector.X);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and y components of the Vector2 and a specified z component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <param name="z">The z component of the resulting Vector3.</param>
        /// <returns>A new Vector3 with the x and y components of the input Vector2 and the specified z component.</returns>
        public static Vector3 ToVector3XY(this Vector2 vector, float z)
            => new(vector.X, vector.Y, z);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and y components of the Vector2 and a z component of 0
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the x and y components of the input Vector2 and the specified z component.</returns>
        public static Vector3 ToVector3XY(this Vector2 vector)
            => vector.ToVector3XY(0);


        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and z components of the Vector2 and a specified y component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <param name="y">The y component of the resulting Vector3.</param>
        /// <returns>A new Vector3 with the x and z components of the input Vector2 and the specified y component.</returns>
        public static Vector3 ToVector3XZ(this Vector2 vector, float y)
            => new(vector.X, y, vector.Y);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the x and z components of the Vector2 and y component of 0.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the x and z components of the input Vector2 and y component of 0.</returns>
        public static Vector3 ToVector3XZ(this Vector2 vector)
            => vector.ToVector3XZ(0);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the y and z components of the Vector2 and a specified x component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <param name="x">The x component of the resulting Vector3.</param>
        /// <returns>A new Vector3 with the y and z components of the input Vector2 and the specified x component.</returns>
        public static Vector3 ToVector3YZ(this Vector2 vector, float x)
            => new(x, vector.X, vector.Y);

        /// <summary>
        /// Converts a Vector2 to a Vector3 with the y and z components of the Vector2 and x component of 0.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the y and z components of the input Vector2 and x component of 0.</returns>
        public static Vector3 ToVector3YZ(this Vector2 vector)
            => vector.ToVector3YZ(0);

        /// <summary>
        /// Returns the reciprocal of the input Vector2 by taking the reciprocal of each component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector2 with the reciprocal of each component of the input Vector2.</returns>
        /// <example>(2f, 3f) => (0.5, 0.33)</example>
        /// <remarks>Reciprocal means dividing 1 by some value (1 / 3) = 0.5</remarks>
        public static Vector2 Reciprocal(this Vector2 vector)
            => new(1f / vector.X, 1f / vector.Y);

        /// <summary>
        /// Multiplies each component of the first Vector2 by the corresponding component of the second Vector2.
        /// </summary>
        /// <param name="first">The first Vector2.</param>
        /// <param name="second">The second Vector2.</param>
        /// <returns>A new Vector2 with the multiplied components of the input Vector2s.</returns>
        public static Vector2 Multiply(this Vector2 first, Vector2 second)
            => new(first.X * second.X, first.Y * second.Y);

        /// <summary>
        /// Clamps the input Vector2 between a minimum and maximum Vector2.
        /// </summary>
        /// <param name="value">The input Vector2 to be clamped.</param>
        /// <param name="minValue">The minimum Vector2.</param>
        /// <param name="maxValue">The maximum Vector2.</param>
        /// <returns>
        /// A new Vector2 where each component is clamped between the corresponding components of the minimum and maximum Vector2.
        /// </returns>
        public static Vector2 Clamp(Vector2 value, Vector2 minValue, Vector2 maxValue)
            => Vector2.Min(Vector2.Max(minValue, value), maxValue);

        /// <summary>
        /// Given a position, a pivot where this position should be, a delta between objects, a count of objects and
        /// the index of the current object, get the position where the current object should be placed in respect to the position.
        /// </summary>
        /// <example>
        /// Given a position of (1, 1) a pivot of (0.5, 0) a delta of (1, 0) and a count of 2.
        /// For index 0 the object will be placed at (0.5, 0) and for index 1 (1.5, 0).
        /// </example>
        public static Vector2 GetPositionWithPivotOffset(Vector2 position, Vector2 pivot, Vector2 delta, int count, int index)
        {
            Vector2 distance = delta * (count - 1);
            Vector2 deltaToFirstObject = -distance.Multiply(pivot);
            Vector2 currentObjectPosition = deltaToFirstObject + index * delta;
            return currentObjectPosition + position;
        }
    }
}
