namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a friendship cannot be found.
    /// </summary>
    public class FriendshipNotFoundException : Exception
    {
        public FriendshipNotFoundException()
            : base("Friendship not found!") { }
        public FriendshipNotFoundException(int id)
            : base($"Friendship not found: {id}") { }
        public FriendshipNotFoundException(string message)
            : base($"Friendship not found: {message}") { }

    }
}
