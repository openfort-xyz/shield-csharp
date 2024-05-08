using System;

namespace Shield.SDK.Errors
{
    public class NoSecretFoundError : Exception
    {
        public NoSecretFoundError(string message)
            : base(message)
        {
            // In C#, the name of the exception class typically serves as the error type,
            // so there's no need to explicitly set a name as in TypeScript.
        }
    }
}
