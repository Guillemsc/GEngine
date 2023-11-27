using System.Collections.Generic;

namespace GEngine.Utils.ActiveSource
{
    public class CompositeActiveSource : IActiveSource
    {
        readonly List<IActiveSource> _activeSources = new();

        public CompositeActiveSource(IEnumerable<IActiveSource> activeSources)
        {
            _activeSources.AddRange(activeSources);
        }

        public void AddActiveSource(IActiveSource activeSource)
        {
            _activeSources.Add(activeSource);
        }

        public void RemoveActiveSource(IActiveSource activeSource)
        {
            _activeSources.Remove(activeSource);
        }

        public void SetActiveAll(object owner, bool active)
        {
            foreach (var activeSource in _activeSources)
            {
                activeSource.SetActiveAll(owner, active);
            }
        }
    }
}
