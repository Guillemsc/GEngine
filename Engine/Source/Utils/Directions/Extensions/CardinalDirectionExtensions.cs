// using System;
// using System.Linq;
// using System.Numerics;
// using Popcore.Core.Maths.Constants;
// using Popcore.Core.Extensions;
// using UnityEngine;
//
// namespace Popcore.Core.Directions
// {
//     public static class CardinalDirectionExtensions
//     {
//         /// <summary>
//         /// From <see cref="CardinalDirection"/> maps to the matching <see cref="OrdinalDirection"/>.
//         /// </summary>
//         public static OrdinalDirection ToOrdinalDirection(this CardinalDirection value)
//             => (OrdinalDirection)((int)value * 2);
//
//         /// <summary>
//         /// Inverts the cardinal direction so it goes to the inverse direction
//         /// </summary>
//         public static CardinalDirection ToInverseCardinalDirection(this CardinalDirection value)
//             => value switch
//             {
//                 CardinalDirection.Up => CardinalDirection.Down,
//                 CardinalDirection.Right => CardinalDirection.Left,
//                 CardinalDirection.Down => CardinalDirection.Up,
//                 CardinalDirection.Left => CardinalDirection.Right,
//                 _ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
//             };
//
//         /// <summary>
//         /// From a <see cref="Vector2"/> direction, gets the <see cref="CardinalDirection"/>.
//         /// </summary>
//         public static CardinalDirection ToCardinalDirection(this Vector2 value)
//         {
//             value = value;
//
//             var dotProductUp = Vector2.Dot(Vector2.up, value);
//             var dotProductRight = Vector2.Dot(Vector2.right, value);
//
//             if (dotProductUp >= MathsConstants.DotProduct45Degrees) return CardinalDirection.Up;
//             if (dotProductUp <= -MathsConstants.DotProduct45Degrees) return CardinalDirection.Down;
//
//             if (dotProductRight >= MathsConstants.DotProduct45Degrees) return CardinalDirection.Right;
//             if (dotProductRight <= -MathsConstants.DotProduct45Degrees) return CardinalDirection.Left;
//
//             return CardinalDirection.Up;
//         }
//
//         /// <summary>
//         /// From a <see cref="Vector2Int"/> direction, gets the <see cref="CardinalDirection"/>.
//         /// </summary>
//         public static CardinalDirection ToCardinalDirection(this Vector2Int value)
//         {
//             value = value.Normalized();
//
//             if (value == Vector2Int.up) return CardinalDirection.Up;
//             if (value == Vector2Int.down) return CardinalDirection.Down;
//
//             if (value == Vector2Int.right) return CardinalDirection.Right;
//             if (value == Vector2Int.left) return CardinalDirection.Left;
//
//             return CardinalDirection.Up;
//         }
//
//         /// <summary>
//         /// Returns the quaternion rotation using the direction, transformed into an euler angle on the X axis.
//         /// </summary>
//         public static Quaternion ToQuaternionX(this CardinalDirection direction)
//             => Quaternion.Euler(direction.ToEulerY());
//
//         /// <summary>
//         /// Returns the quaternion rotation using the direction, transformed into an euler angle on the Y axis.
//         /// </summary>
//         public static Quaternion ToQuaternionY(this CardinalDirection direction)
//             => Quaternion.Euler(direction.ToEulerY());
//
//         /// <summary>
//         /// Returns the quaternion rotation using the direction, transformed into an euler angle on the Z axis.
//         /// </summary>
//         public static Quaternion ToQuaternionZ(this CardinalDirection direction)
//             => Quaternion.Euler(direction.ToEulerZ());
//
//         /// <summary>
//         /// Transforms the direction into an euler angle. Right == 0º.
//         /// </summary>
//         public static int ToEuler(this CardinalDirection direction)
//             => direction switch
//             {
//                 CardinalDirection.Up => 90,
//                 CardinalDirection.Right => 0,
//                 CardinalDirection.Down => 270,
//                 CardinalDirection.Left => 180,
//                 _ => throw new NotImplementedException($"{nameof(CardinalDirection)}")
//             };
//
//         /// <summary>
//         /// Transforms the direction into an euler angle on the X axis. Right == 0º.
//         /// </summary>
//         public static Vector3 ToEulerX(this CardinalDirection direction)
//             => new(direction.ToEuler(), 0, 0);
//
//         /// <summary>
//         /// Transforms the direction into an euler angle on the Y axis. Right == 0º.
//         /// </summary>
//         public static Vector3 ToEulerY(this CardinalDirection direction)
//             => new(0, direction.ToEuler(), 0);
//
//         /// <summary>
//         /// Transforms the direction into an euler angle on the Z axis. Right == 0º.
//         /// </summary>
//         public static Vector3 ToEulerZ(this CardinalDirection direction)
//             => new(0, 0, direction.ToEuler());
//
//         /// <summary>
//         /// Rotates the direction an N number of iterations. Rotation is done to the right.
//         /// </summary>
//         public static CardinalDirection Rotate(this CardinalDirection direction, int iterations)
//         {
//             var index = (int)direction;
//             index = (index + iterations) % 4;
//             return (CardinalDirection)index;
//         }
//
//         /// <summary>
//         /// Rotates the direction 2 iterations.
//         /// </summary>
//         public static CardinalDirection Flip(this CardinalDirection direction) => direction.Rotate(2);
//
//         /// <summary>
//         /// From a <see cref="CardinalDirection"/> direction, gets the <see cref="Vector2Int"/> direction.
//         /// </summary>
//         public static Vector2Int ToVector2Int(this CardinalDirection value)
//             => value switch
//             {
//                 CardinalDirection.Up => Vector2Int.up,
//                 CardinalDirection.Right => Vector2Int.right,
//                 CardinalDirection.Down => Vector2Int.down,
//                 CardinalDirection.Left => Vector2Int.left,
//                 _ => throw new NotImplementedException($"{nameof(CardinalDirection)}")
//             };
//
//         static Vector2Int[] _directionVectors;
//         public static Vector2Int[] DirectionVectors
//             => _directionVectors ??= CardinalDirectionInfo.Values
//                 .Select(direction => direction.ToVector2Int())
//                 .ToArray();
//     }
// }
