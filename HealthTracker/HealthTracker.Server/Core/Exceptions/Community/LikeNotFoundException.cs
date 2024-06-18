namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a like cannot be found.
    /// </summary>
    public class LikeNotFoundException : Exception
    {
        public LikeNotFoundException()
            : base("Like not found!") { }
        public LikeNotFoundException(int id)
            : base($"Like not found: {id}") { }
        public LikeNotFoundException(string message)
            : base($"Like not found: {message}") { }
    }
}
