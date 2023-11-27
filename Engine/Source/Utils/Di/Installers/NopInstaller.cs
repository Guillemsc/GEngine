using GEngine.Utils.Di.Builder;

namespace GEngine.Utils.Di.Installers
{
    public sealed class NopInstaller : IInstaller
    {
        public static readonly NopInstaller Instance = new();

        NopInstaller()
        {

        }

        public void Install(IDiContainerBuilder builder) { }
    }
}
