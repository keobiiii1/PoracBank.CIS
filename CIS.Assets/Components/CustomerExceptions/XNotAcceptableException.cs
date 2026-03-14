namespace Codex.Components.DataModels.CustomExceptions
{
    public class XNotAcceptableException : Exception
    {
        public XNotAcceptableException() { }

        public XNotAcceptableException(string message) : base(message) { }

        public XNotAcceptableException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}