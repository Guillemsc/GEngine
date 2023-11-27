using System.Collections.Generic;
using GEngine.Utils.Validation.Data;
using GEngine.Utils.Validation.Enums;
using GEngine.Utils.Validation.Results;

namespace GEngine.Utils.Validation.Builder
{
    public sealed class ValidationBuilder : IValidationBuilder
    {
        readonly List<IValidationLog> _validationLogs = new List<IValidationLog>();
        ValidationResultType _validationResultType = ValidationResultType.Success;

        public ValidationBuilder()
        {

        }

        public ValidationBuilder(IValidationResult nestedResult)
        {
            _validationResultType &= nestedResult.ValidationResultType;

            _validationLogs.AddRange(nestedResult.ValidationLogs);
        }

        public void LogError(string logMessage)
        {
            _validationLogs.Add(new ValidationLog(ValidationLogType.Error, logMessage));
            _validationResultType = ValidationResultType.Error;
        }

        public void LogWarning(string logMessage)
        {
            _validationLogs.Add(new ValidationLog(ValidationLogType.Warning, logMessage));
        }

        public void LogInfo(string logMessage)
        {
            _validationLogs.Add(new ValidationLog(ValidationLogType.Info, logMessage));
        }

        public IValidationResult Build()
        {
            return new ValidationResult(_validationResultType, _validationLogs);
        }
    }
}
