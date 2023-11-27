using GEngine.Utils.Validation.Data;
using GEngine.Utils.Validation.Enums;

namespace GEngine.Utils.Validation.Results
{
    public interface IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }
    }
}
