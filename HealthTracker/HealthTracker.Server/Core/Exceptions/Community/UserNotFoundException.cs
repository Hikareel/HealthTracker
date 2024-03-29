namespace HealthTracker.Server.Core.Exceptions.Community
{
    /// <summary>
    /// Represents an error when a user cannot be found.
    /// </summary>
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
            : base("User not found!") { }
        public UserNotFoundException(int id)
            : base($"User not found: {id}") { }
        public UserNotFoundException(string message)
            : base($"User not found: {message}") { }


    }
}
