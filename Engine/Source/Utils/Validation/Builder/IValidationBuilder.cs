using GEngine.Utils.Validation.Enums;
using GEngine.Utils.Validation.Results;

namespace GEngine.Utils.Validation.Builder
{
    /// <summary>
    /// Builder for creating validation for assets, scene structure, etc.
    /// </summary>
    public interface IValidationBuilder
    {
        /// <summary>
        /// Adds a validation error. Use when something is wrong enough to prevent usage of the validated object.
        /// Automatically sets validation result <see cref="ValidationResultType"/> to Error.
        /// </summary>
        void LogError(string logMessage);

        /// <summary>
        /// Adds a validation warning. Use when something is wrong, but does not prevent usage of the validated object.
        /// </summary>
        void LogWarning(string logMessage);

        /// <summary>
        /// Adds validation info. Use to show any type of useful info.
        /// </summary>
        void LogInfo(string logMessage);

        /// <summary>
        /// Generates the <see cref="IValidationResult"/>, which contains all the validation logs and
        /// the final <see cref="ValidationResultType"/>.
        /// </summary>
        IValidationResult Build();
    }
}
