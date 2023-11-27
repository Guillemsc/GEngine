using System.Collections.Generic;
using GEngine.Utils.Validation.Data;
using GEngine.Utils.Validation.Enums;

namespace GEngine.Utils.Validation.Results
{
    public sealed class ValidationResult : IValidationResult
    {
        public ValidationResultType ValidationResultType { get; }
        public IReadOnlyList<IValidationLog> ValidationLogs { get; }

        public ValidationResult(ValidationResultType validationResultType, IReadOnlyList<IValidationLog> validationLogs)
        {
            ValidationResultType = validationResultType;
            ValidationLogs = validationLogs;
        }
    }
}
