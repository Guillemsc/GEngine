using System;

namespace GEngine.Utils.Types
{
    /// <summary>
    /// Represents a simple string error message.
    /// Useful when used with discriminated unions.
    /// </summary>
    public readonly struct ErrorMessage
    {
        public string Message { get; }

        public ErrorMessage(string message)
        {
            Message = message;
        }

        public ErrorMessage(Exception exception)
        {
            Message = exception.Message;
        }

        public override string ToString() => Message;
    }
}
