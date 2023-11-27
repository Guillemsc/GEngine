using System;
using System.Collections.Generic;
using System.Linq;

namespace GEngine.Utils.Extensions
{
    public static class GraphTraversalExtensions
    {
        public readonly struct DepthDistanceElement<TElement>
        {
            public TElement Element { get; }
            public int Distance { get; }

            public DepthDistanceElement(TElement element, int distance)
            {
                Element = element;
                Distance = distance;
            }
        }

        /// <summary>
        /// Does a breadth search and returns the elements that are no more further than maxDistance
        /// </summary>
        public static List<DepthDistanceElement<TElement>> GetNodesWithinDistance<TElement>(TElement rootElement,
            int maxDistance,
            Func<TElement, IEnumerable<TElement>> getSiblingsFunc,
            bool includeRoot)
        {
            Dictionary<TElement, int> visitedDistance = new Dictionary<TElement, int>();
            Stack<(TElement, int)> toVisit = new Stack<(TElement, int)>();

            toVisit.Push((rootElement, 0));

            while (toVisit.Count > 0)
            {
                (var element, var distance) = toVisit.Pop();

                if (distance > maxDistance)
                {
                    continue;
                }

                if (visitedDistance.TryGetValue(element, out var storedDistance))
                {
                    if (storedDistance <= distance)
                    {
                        continue;
                    }

                    visitedDistance[element] = distance;
                }

                visitedDistance[element] = distance;

                var nextDistance = distance + 1;

                foreach (var sibling in getSiblingsFunc.Invoke(element))
                {
                    toVisit.Push((sibling, nextDistance));
                }
            }

            if (!includeRoot)
            {
                visitedDistance.Remove(rootElement);
            }

            return visitedDistance
                .Select(x => new DepthDistanceElement<TElement>(x.Key, x.Value))
                .ToList();
        }

        /// <summary>
        /// Does a breadth first search of the elements and then returns an enumerable of enumerables
        /// where the first enumerable returns an enumerable for each depth level
        /// and the second enumerable return each element of the depth level
        /// </summary>
        public static IEnumerable<IEnumerable<TElement>> GetNodesWithinDistanceEnumeratedByDistance<TElement>(
            TElement rootElement,
            int maxDistance,
            Func<TElement, IEnumerable<TElement>> getSiblingsFunc,
            bool includeRoot)
        {
            return GetNodesWithinDistance(rootElement, maxDistance, getSiblingsFunc, includeRoot)
                .GroupBy(x => x.Distance)
                .OrderBy(x => x.Key)
                .Select(x => x.Select(y => y.Element));
        }
    }
}
