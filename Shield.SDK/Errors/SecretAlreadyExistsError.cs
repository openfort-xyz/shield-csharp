using System;

namespace Shield.SDK.Errors
{
    public class SecretAlreadyExistsError : Exception
    {
        public SecretAlreadyExistsError(string message)
            : base(message)
        {
            // C# uses the exception class name to identify the type of exception,
            // so there's no need to explicitly set a name as in TypeScript.
        }
    }
}
