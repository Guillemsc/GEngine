using GEngine.Utils.Validation.Enums;

namespace GEngine.Utils.Validation.Data
{
    public interface IValidationLog
    {
        public ValidationLogType ValidationLogType { get; }
        public string LogMessage { get; }
    }
}
