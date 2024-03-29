namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a comment cannot be found.
    /// </summary>
    public class CommentNotFoundException : Exception
    {
        public CommentNotFoundException()
            : base("Comment not found!") { }
        public CommentNotFoundException(int id)
            : base($"Comment not found: {id}") { }
        public CommentNotFoundException(string message)
            : base($"Comment not found: {message}") { }
    }
}
