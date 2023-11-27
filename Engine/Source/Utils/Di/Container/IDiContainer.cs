using System;
using System.Collections.Generic;
using GEngine.Utils.Di.Bindings;

namespace GEngine.Utils.Di.Container
{
    /// <summary>
    /// Contains a mapping of how registered objects need to be created, including their dependencies.
    /// </summary>
    public interface IDiContainer : IDiResolveContainer, IDisposable
    {

    }
}
