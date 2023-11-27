// using System;
// using System.Linq;
// using UnityEngine;
//
// namespace Popcore.Core.Directions
// {
//     public static class OrdinalDirectionExtensions
//     {
//         /// <summary>
//         /// From <see cref="OrdinalDirection"/> maps to the matching <see cref="CardinalDirection"/>.
//         /// If the direction is a transition direction (<see cref="OrdinalDirection.UpRight"/>,
//         /// <see cref="OrdinalDirection.UpLeft"/>, <see cref="OrdinalDirection.DownRight"/>,
//         /// <see cref="OrdinalDirection.DownLeft"/>), the returned value is the closest non transition direction,
//         /// rotating to the left.
//         /// </summary>
//         public static CardinalDirection ToCardinalDirection(this OrdinalDirection value)
//             => (CardinalDirection)((int)value / 2);
//
//         /// <summary>
//         /// From a <see cref="OrdinalDirection"/> direction, gets the <see cref="Vector2Int"/> direction.
//         /// If the direction is a transition direction (<see cref="OrdinalDirection.UpRight"/>,
//         /// <see cref="OrdinalDirection.UpLeft"/>, <see cref="OrdinalDirection.DownRight"/>,
//         /// <see cref="OrdinalDirection.DownLeft"/>), the two directions composing the transition get added.
//         /// </summary>
//         public static Vector2Int ToVector2Int(this OrdinalDirection value)
//             => value switch
//             {
//                 OrdinalDirection.Up => Vector2Int.up,
//                 OrdinalDirection.Right => Vector2Int.right,
//                 OrdinalDirection.Down => Vector2Int.down,
//                 OrdinalDirection.Left => Vector2Int.left,
//                 OrdinalDirection.UpLeft => Vector2Int.up + Vector2Int.left,
//                 OrdinalDirection.UpRight => Vector2Int.up + Vector2Int.right,
//                 OrdinalDirection.DownLeft => Vector2Int.down + Vector2Int.left,
//                 OrdinalDirection.DownRight => Vector2Int.down + Vector2Int.left,
//                 _ => throw new NotImplementedException($"{nameof(OrdinalDirection)}")
//             };
//
//         static Vector2Int[] _directionVectors;
//         public static Vector2Int[] DirectionVectors
//             => _directionVectors ??= OrdinalDirectionInfo.Values
//                 .Select(direction => direction.ToVector2Int())
//                 .ToArray();
//     }
// }
