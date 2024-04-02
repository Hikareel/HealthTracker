namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a post cannot be found.
    /// </summary>
    public class PostNotFoundException : Exception
    {
        public PostNotFoundException()
            : base("Post not found!") { }
        public PostNotFoundException(int id)
            : base($"Post not found: {id}") { }
        public PostNotFoundException(string message)
            : base($"Post not found: {message}") { }
    }
}
