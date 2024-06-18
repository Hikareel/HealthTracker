namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a like already exists cannot be found.
    /// </summary>
    public class LikeAlreadyExistsException : Exception
    {
        public LikeAlreadyExistsException()
            : base("Like already exists!") { }
        public LikeAlreadyExistsException(int id)
            : base($"Like already exists: {id}") { }
        public LikeAlreadyExistsException(string message)
            : base($"Like already exists: {message}") { }
    }
}
