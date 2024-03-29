namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when friendship already exists.
    /// </summary>
    public class FriendshipAlreadyExistsException : Exception
    {
        public FriendshipAlreadyExistsException()
            : base("Friendship already exists!") { }

    }
}
