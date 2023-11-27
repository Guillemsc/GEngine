using System.Threading;
using System.Threading.Tasks;

namespace GEngine.Utils.Sequencing.Instructions
{
    public interface IInstruction
    {
        Task Execute(CancellationToken cancellationToken);
    }
}
