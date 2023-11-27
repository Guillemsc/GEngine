using GEngine.Utils.Validation.Enums;

namespace GEngine.Utils.Validation.Data
{
    public sealed class ValidationLog : IValidationLog
    {
        public ValidationLogType ValidationLogType { get; }
        public string LogMessage { get; }

        public ValidationLog(ValidationLogType validationLogType, string logMessage)
        {
            ValidationLogType = validationLogType;
            LogMessage = logMessage;
        }
    }
}
