using System.Numerics;

namespace GEngine.Utils.Extensions
{
    public static class Vector3Extensions
    {
        /// <summary>
        /// Equivalent to x:<see cref="float.MaxValue"/> y:<see cref="float.MaxValue"/> z:<see cref="float.MaxValue"/>.
        /// </summary>
        public static readonly Vector3 MaxValue = new(float.MaxValue, float.MaxValue, float.MaxValue);

        /// <summary>
        /// Equivalent to x:<see cref="float.MinValue"/> y:<see cref="float.MinValue"/> z:<see cref="float.MinValue"/>.
        /// </summary>
        public static readonly Vector3 MinValue = new(float.MinValue, float.MinValue, float.MinValue);

        /// <summary>
        /// Equivalent to x:0.5f y:0.5f y:0.5f.
        /// </summary>
        public static readonly Vector3 HalfOne = new(0.5f, 0.5f, 0.5f);

        // /// <summary>
        // /// Calculates the Manhattan distance between two <see cref="Vector3"/> objects, treating their components as integers.
        // /// </summary>
        // /// <param name="a">The first <see cref="Vector3"/> object.</param>
        // /// <param name="b">The second <see cref="Vector3"/> object.</param>
        // /// <returns>The Manhattan distance between the two vectors.</returns>
        // /// <remarks>Manhattan distance is calculated by
        // /// taking the sum of absoulte distances between the x, the y and the z coordinates.</remarks>
        // public static int ManhattanDistanceInt(this Vector3 a, Vector3 b)
        //     => a.ToVector3Int().ManhattanDistance(b.ToVector3Int());

        /// <summary>
        /// Checks if two <see cref="Vector3"/> are approximately equal within a small <see cref="Mathf.Epsilon"/> value.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/> object.</param>
        /// <param name="b">The second <see cref="Vector3"/> object.</param>
        /// <returns><c>true</c> if the vectors are approximately equal; otherwise, <c>false</c>.</returns>
        // public static bool AreEpsilonEquals(Vector3 a, Vector3 b)
        //     => MathExtensions.IsEpsilonEqualsZero((a - b).sqrMagnitude);
        //
        // /// <summary>
        // /// Checks if the magnitude of a <see cref="Vector3"/> is approximately equal
        // /// to zero within a small <see cref="Mathf.Epsilon"/> value.
        // /// </summary>
        // /// <param name="a">The <see cref="Vector3"/> object.</param>
        // /// <returns><c>true</c> if the vector is approximately equal to zero; otherwise, <c>false</c>.</returns>
        // public static bool IsEpsilonEqualsZero(Vector3 a)
        //     => MathExtensions.IsEpsilonEqualsZero(a.sqrMagnitude);

        /// <summary>
        /// Returns a new Vector3 with each component (x, y, z) set to the
        /// absolute value of the corresponding.
        /// </summary>
        /// <param name="vector">The input Vector3.</param>
        /// <returns>A new Vector3 with absolute values.</returns>
        public static Vector3 Abs(this Vector3 vector)
            => new(vector.X.Abs(), vector.Y.Abs(), vector.Z.Abs());

        /// <summary>
        /// Given a position, a pivot where this position should be, a delta between objects, a count of objects and
        /// the index of the current object, get the position where the current object should be placed in respect to the position
        /// </summary>
        /// <example>
        /// Given a position of (1,1,1) a pivot of (0.5, 0, 0) a delta of (1,0,0) and a count of 2
        /// For index 0 the object will be placed at (0.5, 0, 0) and for index 1 (1.5, 0, 0)
        /// </example>
        public static Vector3 GetPositionWithPivotOffset(Vector3 position, Vector3 pivot, Vector3 delta, int count, int index)
        {
            Vector3 distance = delta * (count - 1);
            Vector3 deltaToFirstObject = -distance.Multiply(pivot);
            Vector3 currentObjectPosition = deltaToFirstObject + index * delta;
            return currentObjectPosition + position;
        }

        /// <summary>
        /// Calculates the absolute distance between two <see cref="Vector3"/> objects.
        /// </summary>
        /// <param name="a">The first <see cref="Vector3"/> object.</param>
        /// <param name="b">The second <see cref="Vector3"/> object.</param>
        /// <returns>The absolute distance between the two vectors.</returns>
        public static float AbsDistance(Vector3 a, Vector3 b)
            => Math.Abs(Vector3.Distance(a, b));

        /// <summary>
        /// Returns the reciprocal of the input Vector3 by taking the reciprocal of each component.
        /// </summary>
        /// <param name="vector">The input Vector2.</param>
        /// <returns>A new Vector3 with the reciprocal of each component of the input Vector3.</returns>
        /// <example>(2f, 3f, 5f) => (0.5f, 0.33f, 0.2f)</example>
        /// <remarks>Reciprocal means dividing 1 by some value (1 / 3) = 0.5</remarks>
        public static Vector3 Reciprocal(this Vector3 vector)
            => new(1f / vector.X, 1f / vector.Y, 1f / vector.Z);

        /// <summary>
        /// Calculates a new <see cref="Vector3"/> object by rotating the input vector clockwise by 90 degrees in the XY plane.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object obtained by rotating the input vector
        /// clockwise by 90 degrees in the XY plane.</returns>
        public static Vector3 PerpendicularClockwiseXY(this Vector3 vector)
            => new(vector.Y, -vector.X, vector.Z);

        /// <summary>
        /// Calculates a new <see cref="Vector3"/> object by rotating the input vector counter-clockwise by 90 degrees in the XY plane.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object obtained by rotating the input vector counter-clockwise
        /// by 90 degrees in the XY plane.</returns>
        public static Vector3 PerpendicularCounterClockwiseXY(this Vector3 vector)
            => new(-vector.Y, vector.X, vector.Z);

        /// <summary>
        /// Calculates a new <see cref="Vector3"/> object by rotating the input vector clockwise by 90 degrees in the XZ plane.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object obtained by rotating the input vector
        /// clockwise by 90 degrees in the XZ plane.</returns>
        public static Vector3 PerpendicularClockwiseXZ(this Vector3 vector)
            => new(vector.Z, vector.Y, -vector.X);

        /// <summary>
        /// Calculates a new <see cref="Vector3"/> object by rotating the input vector counter-clockwise by 90 degrees in the XZ plane.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object obtained by rotating the input
        /// vector counter-clockwise by 90 degrees in the XZ plane.</returns>
        public static Vector3 PerpendicularCounterClockwiseXZ(this Vector3 vector)
            => new(-vector.Z, vector.Y, vector.X);

        /// <summary>
        /// Calculates a new <see cref="Vector3"/> object by rotating the input vector clockwise by 90 degrees in the YZ plane.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object obtained by rotating the input
        /// vector clockwise by 90 degrees in the YZ plane.</returns>
        public static Vector3 PerpendicularClockwiseYZ(this Vector3 vector)
            => new(vector.X, vector.Z, -vector.Y);

        /// <summary>
        /// Calculates a new <see cref="Vector3"/> object by rotating the input vector counter-clockwise by 90 degrees in the YZ plane.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object obtained by rotating the input
        /// vector counter-clockwise by 90 degrees in the YZ plane.</returns>
        public static Vector3 PerpendicularCounterClockwiseYZ(this Vector3 vector)
            => new(vector.X, -vector.Z, vector.Y);

        /// <summary>
        /// Converts a <see cref="Vector3"/> object to a <see cref="Vector2"/> object, discarding the z-component.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector2"/> object with the x and y components of the input vector.</returns>
        public static Vector2 ToVector2XY(this Vector3 vector)
            => new(vector.X, vector.Y);

        /// <summary>
        /// Converts a <see cref="Vector3"/> object to a <see cref="Vector2"/> object, discarding the y-component.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector2"/> object with the x and z components of the input vector.</returns>
        public static Vector2 ToVector2XZ(this Vector3 vector)
            => new(vector.X, vector.Z);
        
        public static Quaternion ToQuaternionFromDegrees(this Vector3 vector)
            => Quaternion.CreateFromYawPitchRoll(
                vector.X * MathExtensions.Deg2Rad,
                vector.Y * MathExtensions.Deg2Rad,
                vector.Z * MathExtensions.Deg2Rad
            );
        
        public static Quaternion ToQuaternionFromRadiants(this Vector3 vector)
            => Quaternion.CreateFromYawPitchRoll(
                vector.X,
                vector.Y,
                vector.Z
            );
        
        /// <summary>
        /// Clamps a <see cref="Vector3"/> object between specified minimum and maximum values for each component (x, y, z).
        /// </summary>
        /// <param name="value">The input <see cref="Vector3"/> object to be clamped.</param>
        /// <param name="minValue">The minimum <see cref="Vector3"/> object for clamping.</param>
        /// <param name="maxValue">The maximum <see cref="Vector3"/> object for clamping.</param>
        /// <returns>A new <see cref="Vector3"/> object with each component clamped between the corresponding
        /// components of <paramref name="minValue"/> and <paramref name="maxValue"/>.</returns>
        public static Vector3 Clamp(Vector3 value, Vector3 minValue, Vector3 maxValue)
            => Vector3.Min(Vector3.Max(minValue, value), maxValue);

        // /// <summary>
        // /// Clamps an object that has some size and is positioned according to a pivot on that size between a min and a max value
        // /// See tests at TestVector3Extensions for samples
        // /// </summary>
        // public static Vector3 ClampSizePivot(Vector3 value, Vector3 size, Vector3 pivot, Vector3 minValue, Vector3 maxValue)
        // {
        //     Vector3 valuePivotMin = value - size.Multiply(pivot);
        //     Vector3 valuePivotMax = value + size.Multiply(Vector3.one - pivot);
        //
        //     Vector3 adjustMin = Vector3.Max(minValue - valuePivotMin, Vector3.zero);
        //     Vector3 adjustMax = Vector3.Min(maxValue - valuePivotMax, Vector3.zero);
        //     Vector3 adjust = adjustMin + adjustMax;
        //
        //     return value + adjust;
        // }
        //
        // /// <summary>
        // /// Divides each component of the numerator <see cref="Vector3"/> by the corresponding component of the
        // /// denominator <see cref="Vector3"/>.
        // /// </summary>
        // /// <param name="numerator">The numerator <see cref="Vector3"/> object.</param>
        // /// <param name="denominator">The denominator <see cref="Vector3"/> object.</param>
        // /// <returns>A new <see cref="Vector3"/> object with each component being the result of dividing the
        // /// corresponding component of the numerator by the denominator.</returns>
        // public static Vector3 Divide(this Vector3 numerator, Vector3 denominator)
        //     => new(
        //         MathExtensions.Divide(numerator.x, denominator.x),
        //         MathExtensions.Divide(numerator.y, denominator.y),
        //         MathExtensions.Divide(numerator.z, denominator.z)
        //     );

        /// <summary>
        /// Multiplies each component of the first <see cref="Vector3"/> by the corresponding component of
        /// the second <see cref="Vector3"/>.
        /// </summary>
        /// <param name="first">The first <see cref="Vector3"/> object.</param>
        /// <param name="second">The second <see cref="Vector3"/> object.</param>
        /// <returns>A new <see cref="Vector3"/> object with each component being the result of multiplying
        /// the corresponding component of the first by the second.</returns>
        public static Vector3 Multiply(this Vector3 first, Vector3 second)
            => new(
                first.X * second.X,
                first.Y * second.Y,
                first.Z * second.Z
            );

        // /// <summary>
        // /// Converts a <see cref="Vector3"/> object to a <see cref="Quaternion"/> object using Euler angles for rotation.
        // /// </summary>
        // /// <param name="value">The input <see cref="Vector3"/> object representing Euler angles.</param>
        // /// <returns>A new <see cref="Quaternion"/> object representing the rotation specified by the Euler angles.</returns>
        // public static Quaternion ToEulerQuaternion(this Vector3 value)
        //     => Quaternion.Euler(value.x, value.y, value.z);

        /// <summary>
        /// Returns the maximum component (x, y or z) value of a <see cref="Vector3"/> object.
        /// </summary>
        /// <param name="value">The input <see cref="Vector3"/> object.</param>
        /// <returns>The maximum component value of the input vector.</returns>
        public static float MaxComponent(this Vector3 value)
            => Math.Max(value.X, Math.Max(value.X, value.Y));

        /// <summary>
        /// Returns the minimum component (x, y or z) value of a <see cref="Vector3"/> object.
        /// </summary>
        /// <param name="value">The input <see cref="Vector3"/> object.</param>
        /// <returns>The minimum component value of the input vector.</returns>
        public static float MinComponent(this Vector3 value)
            => Math.Min(value.X, Math.Min(value.Y, value.Z));

        /// <summary>
        /// Creates a new <see cref="Vector3"/> object with the x-component replaced by the specified value.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <param name="x">The new value for the x-component.</param>
        /// <returns>A new <see cref="Vector3"/> object with the x-component replaced by the specified value.</returns>
        public static Vector3 WithX(this Vector3 vector, float x) => new(x, vector.Y, vector.Z);

        /// <summary>
        /// Creates a new <see cref="Vector3"/> object with the y-component replaced by the specified value.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <param name="y">The new value for the y-component.</param>
        /// <returns>A new <see cref="Vector3"/> object with the y-component replaced by the specified value.</returns>
        public static Vector3 WithY(this Vector3 vector, float y) => new(vector.X, y, vector.Z);

        /// <summary>
        /// Creates a new <see cref="Vector3"/> object with the z-component replaced by the specified value.
        /// </summary>
        /// <param name="vector">The input <see cref="Vector3"/> object.</param>
        /// <param name="z">The new value for the z-component.</param>
        /// <returns>A new <see cref="Vector3"/> object with the z-component replaced by the specified value.</returns>
        public static Vector3 WithZ(this Vector3 vector, float z) => new(vector.X, vector.Y, z);
    }
}
