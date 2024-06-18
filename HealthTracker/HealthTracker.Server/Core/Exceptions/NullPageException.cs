namespace HealthTracker.Server.Core.Exceptions
{
    /// <summary>
    /// Represents an error when a user cannot be found.
    /// </summary>
    public class NullPageException : Exception
    {
        public NullPageException()
            : base("The page contains no content!") { }
        public NullPageException(int number)
            : base($"The page {number} contains no content") { }
        public NullPageException(string message)
            : base($"The page contains no content: {message}") { }


    }
}
